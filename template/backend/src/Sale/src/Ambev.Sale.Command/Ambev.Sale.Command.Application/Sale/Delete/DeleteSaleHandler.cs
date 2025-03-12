using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Command.Domain.Specification;

namespace Ambev.Sale.Command.Application.Sale.Delete
{
    /// <summary>
    /// Handle delete sale
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleCommandRepository _repositorycommand;
        private readonly ISaleQueryRepository _repositoryquery;
        private readonly IMapper _mapper;
        private readonly IMessageBus _bus;

        public DeleteSaleHandler(IMessageBus bus, ISaleQueryRepository saleQueryRepository, ISaleCommandRepository repository, IMapper mapper)
        {
            _bus = bus;
            _repositoryquery = saleQueryRepository;
            _repositorycommand = repository;
            _mapper = mapper;
        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator(_repositoryquery);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var record = await _repositoryquery.GetByIdAsync(command.Id);

            //if cancelled, cannot delete 
            var cancelsalespec = new SaleCancelSpecification();
            if (cancelsalespec.IsSatisfiedBy(record))
                throw new Exception($"Sale with ID {command.Id} already cancelled.");

            record.Status = Domain.Enum.SaleStatus.Cancelled;

            //delete salve
            var update = await _repositorycommand.DeleteAsync(record);

            //call eventbus to delete sale in database read
            await _bus.PublishAsync(new SaleDeletedEvent
            {
                Id = command.Id
            });

            return _mapper.Map<DeleteSaleResult>(update);
        }
    }
}
