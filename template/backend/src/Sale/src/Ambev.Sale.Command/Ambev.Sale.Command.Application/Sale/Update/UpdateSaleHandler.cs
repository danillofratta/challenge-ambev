using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Contracts.Dto;
using Ambev.Sale.Query.Domain.Enum;
using Ambev.Sale.Command.Domain.Specification;

namespace Ambev.Sale.Command.Application.Sale.Update
{
    /// <summary>
    /// Update only basic data from sale
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleCommandRepository _repositorycommnad;
        private readonly ISaleQueryRepository _repositoryquery;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;

        public UpdateSaleHandler(ISaleQueryRepository repositoryquery, IMessageBus bus, IMediator mediator, ISaleCommandRepository repository, IMapper mapper)
        {
            _repositoryquery = repositoryquery;
            _repositorycommnad = repository;
            _mapper = mapper;
            _mediator = mediator;
            _bus = bus;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator(_repositoryquery);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            //check sale exist
            var existingSale = await _repositoryquery.GetByIdAsync(command.Id);
            if (existingSale == null)
                throw new Exception($"Sale with ID {command.Id} not found.");

            //check sale is cancelled
            var cancelsalespec = new SaleCancelSpecification();
            if (cancelsalespec.IsSatisfiedBy(existingSale))            
                throw new Exception($"Sale with ID {command.Id} already cancelled.");                           

            _mapper.Map(command, existingSale);            

            //update sale
            var update = await _repositorycommnad.UpdateAsync(existingSale);
            var result = _mapper.Map<UpdateSaleResult>(update);
                        
            //call eventbus to update sale in database read
            await _bus.PublishAsync(new SaleUpdatedEvent
            {
                Id = update.Id,
                Number = update.Number,
                CustomerId = update.CustomerId,
                CustomerName = update.CustomerName ?? string.Empty,
                BranchId = update.BranchId,
                BranchName = update.BranchName ?? string.Empty,
                TotalAmount = update.TotalAmount,
                CreatedAt = update.CreatedAt,
                Status = (SaleStatusDto)update.Status,
                SaleItens = _mapper.Map<List<SaleItemDto>>(update.SaleItens)
            });

            return result;
        }
    }
}
