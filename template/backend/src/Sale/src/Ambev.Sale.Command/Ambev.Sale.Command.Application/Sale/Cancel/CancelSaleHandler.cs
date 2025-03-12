using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;
using Ambev.Sale.Command.Application.Sale.Create;
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Command.Domain.Specification;

namespace Ambev.Sale.Command.Application.Sale.Cancel
{
    /// <summary>
    /// Handle cancel sale
    /// </summary>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleCommandRepository _repositorycommand;
        private readonly ISaleQueryRepository _repositoryquery;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;

        public CancelSaleHandler(IMediator mediator, ISaleCommandRepository repositorycommand, ISaleQueryRepository saleQueryRepository, IMapper mapper, IMessageBus bus)
        {
            _bus = bus;
            _repositoryquery = saleQueryRepository;
            _repositorycommand = repositorycommand;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleCommandValidator(_repositoryquery);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);            

            var record = await _repositoryquery.GetByIdAsync(command.id);

            ///check is cancelled
            var cancelsalespec = new SaleCancelSpecification();
            if (cancelsalespec.IsSatisfiedBy(record))
                throw new Exception($"Sale with ID {record.Id} already cancelled.");

            record.Cancel();
            var update = await _repositorycommand.UpdateAsync(record);

            ////call eventbus to cancel sale in database read
            await _bus.PublishAsync(new SaleCanceledEvent
            {
                Id = update.Id               
            });

            return _mapper.Map<CancelSaleResult>(update); ;
        }
    }
}
