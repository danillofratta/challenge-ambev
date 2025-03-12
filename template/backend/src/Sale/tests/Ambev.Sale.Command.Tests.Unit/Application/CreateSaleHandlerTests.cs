using AutoMapper;
using MediatR;
using Ambev.Sale.Core.Domain.Service;
using Ambev.Tests.Unit.Data;
using NSubstitute;
using Ambev.Sale.Command.Application.Sale.Create;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;
using Ambev.Base.Domain.Entities;
using Ambev.Sale.Contracts.Events;
using FluentAssertions;

namespace Ambev.Tests.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleCommandRepository _repository;
    private readonly IMapper _mapper;
    private readonly SaleDiscountService _discountService;
    private readonly IMediator _mediator;
    private readonly IMessageBus _bus;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _repository = Substitute.For<ISaleCommandRepository>();
        _mapper = Substitute.For<IMapper>();
        _discountService = Substitute.For<SaleDiscountService>();
        _mediator = Substitute.For<IMediator>();
        _bus = Substitute.For<IMessageBus>();

        _handler = new CreateSaleHandler(_bus, _mediator, _discountService, _repository, _mapper);
    }

    /// <summary>
    /// Check if create sale
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var saleentity = new Sale.Command.Domain.Entities.Sale
        {
            Id = Guid.NewGuid(),
            Number = 0,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName,
            TotalAmount = command.SaleItens.Sum(i => i.UnitPrice * i.Quantity),
            Status = Ambev.Sale.Command.Domain.Enum.SaleStatus.NotCancelled,
            SaleItens = command.SaleItens.Select(i => new Sale.Command.Domain.Entities.SaleItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.UnitPrice * i.Quantity,
                Status = Ambev.Sale.Command.Domain.Enum.SaleItemStatus.NotCancelled
            }).ToList()
        };

        var saleResult = new CreateSaleResult
        {
            Id = saleentity.Id,
            Number = saleentity.Number
        };

        _mapper.Map<Sale.Command.Domain.Entities.Sale>(command).Returns(saleentity);
        _mapper.Map<CreateSaleResult>(saleentity).Returns(saleResult);

        _repository.SaveAsync(Arg.Any<Sale.Command.Domain.Entities.Sale>())
            .Returns(saleentity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(saleResult.Id, result.Id);
        Assert.Equal(saleResult.Number, result.Number);

        // Verify interactions        
        await _repository.Received(1).SaveAsync(saleentity);
        await _mediator.Received(1).Publish(Arg.Any<CreateSaleResult>());
        await _bus.Received(1).PublishAsync(Arg.Any<SaleCreatedEvent>());
    }

    [Fact(DisplayName = "Given invalid sale data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Check if sala have more then 20 identical itens
    /// </summary>
    [Fact]
    public void ValidateQuantity_Should_Throw_Exception_When_Exceeding_Max_Quantity()
    {
        // Arrange
        var service = new SaleDiscountService();

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => service.ValidateQuantity(25));
        Assert.Equal("Cannot sell more than 20 identical items", exception.Message);
    }
}
