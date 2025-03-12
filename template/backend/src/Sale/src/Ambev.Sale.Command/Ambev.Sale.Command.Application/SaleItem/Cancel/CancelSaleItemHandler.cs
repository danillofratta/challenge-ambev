using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;
using Ambev.Sale.Command.Application.Sale.Create;
using Ambev.Sale.Contracts.Events.SaleItem;
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Domain.Enum;
using Ambev.Sale.Contracts.Dto;
using Ambev.Sale.Command.Domain.Specification;

namespace Ambev.Sale.Command.Application.SaleItem.Cancel
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly ISaleQueryRepository _repositorysalequery;
        private readonly ISaleCommandRepository _repositorysaleCommand;
        private readonly ISaleItemQueryRepository _repositorysaleitemquery;
        private readonly ISaleItemCommandRepository _repositorysaleitemcommand;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;
        private readonly SaleRecalculationService _recalculationService;

        public CancelSaleItemHandler(IMessageBus bus, ISaleItemCommandRepository saleItemCommandRepository, ISaleQueryRepository repositorysale, ISaleCommandRepository saleCommandRepository, SaleRecalculationService recalculationService, IMediator mediator, ISaleItemQueryRepository repositorysaleitemquery, IMapper mapper)
        {
            _bus = bus;
            _repositorysaleitemquery = repositorysaleitemquery;
            _mapper = mapper;
            _mediator = mediator;
            _recalculationService = recalculationService;
            _repositorysalequery = repositorysale;
            _repositorysaleCommand = saleCommandRepository;
            _repositorysaleitemcommand = saleItemCommandRepository;
        }

        public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleItemCommandValidator(_repositorysaleitemquery);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);            
            if (validationResult != null && !validationResult.IsValid)                
                throw new ValidationException(validationResult.Errors);
            
            var record = await _repositorysaleitemquery.GetByIdAsync(command.id);
            
            var cancelsalespec = new SaleItemCancelSpecification();
            if (cancelsalespec.IsSatisfiedBy(record))
                throw new Exception($"Sale Item with ID {record.Id} already cancelled.");

            record.Status = Ambev.Sale.Command.Domain.Enum.SaleItemStatus.Cancelled;

            var update = await _repositorysaleitemcommand.UpdateAsync(record);
            
            //after cancel item, recalculate itens of sale and total of sale
            var sale = await _repositorysalequery.GetByIdAsync(record.SaleId);
            _recalculationService.RecalculateSale(sale);
            //save sale with new total
            await _repositorysaleCommand.UpdateAsync(sale);

            //update sale item
            await _bus.PublishAsync(new SaleItemCanceledEvent
            {
                Id = update.Id
            });

            //update sale becausa recalculate. 
            //TODO: update only the field that was modified
            await _bus.PublishAsync(new SaleUpdatedEvent
            {
                Id = sale.Id,
                Number = sale.Number,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName ?? string.Empty,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName ?? string.Empty,
                TotalAmount = sale.TotalAmount,
                CreatedAt = sale.CreatedAt,
                Status = (SaleStatusDto)sale.Status,
                SaleItens = _mapper.Map<List<SaleItemDto>>(sale.SaleItens)
            });

            return _mapper.Map<CancelSaleItemResult>(update); ;            
        }
    }
}
