using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;

namespace Ambev.Sale.Command.Application.Sale.Update
{
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

            var record = _mapper.Map<Domain.Entities.Sale>(command);

            var update = await _repositorycommnad.UpdateAsync(record);
            var result = _mapper.Map<UpdateSaleResult>(update);

            //publich event 
            await _mediator.Publish(new UpdateSaleResult
            {
                Id = result.Id,
                Number = result.Number
            });
            await Task.FromResult("Sale Modified");

            //todo
            //using rebus
            //await _bus.PublishAsync(new CreateSaleEvent
            //{
            //    Id = created.Id,
            //    Number = created.Number,
            //    CustomerId = created.CustomerId,
            //    BranchId = created.BranchId,
            //    TotalAmount = created.TotalAmount,
            //    CreatedAt = created.CreatedAt
            //});

            return result;
        }
    }
}
