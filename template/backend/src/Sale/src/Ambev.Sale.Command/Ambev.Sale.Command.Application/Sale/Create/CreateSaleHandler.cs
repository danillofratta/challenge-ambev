using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Service;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;

namespace Ambev.Sale.Command.Application.Sale.Create
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleCommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly SaleDiscountService _discountService;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;

        public CreateSaleHandler(IMessageBus bus, IMediator mediator, SaleDiscountService discountService, ISaleCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _discountService = discountService;
            _mediator = mediator;
            _bus = bus;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var record = _mapper.Map<Domain.Entities.Sale>(command);
            //todo 
            record.Status = Domain.Enum.SaleStatus.NotCancelled;
            record.SaleItens.ForEach(x => x.Status = Domain.Enum.SaleItemStatus.NotCancelled);

            _discountService.ValidateSaleItems(record.SaleItens);
            if (_discountService.IsValid)
            {
                //todo verify to put on domain
                record.TotalAmount = record.SaleItens.Sum(x => x.TotalPrice);

                var created = await _repository.SaveAsync(record);
                var result = _mapper.Map<CreateSaleResult>(created);

                //publich event 
                await _mediator.Publish(new CreateSaleResult
                {
                    Id = result.Id,
                    Number = result.Number
                });
                await Task.FromResult("Sale Created");

                //using rebus
                await _bus.PublishAsync(new CreateSaleEvent
                {
                    Id = created.Id,
                    Number = created.Number,
                    CustomerId = created.CustomerId,
                    BranchId = created.BranchId,
                    TotalAmount = created.TotalAmount,
                    CreatedAt = created.CreatedAt
                });

                return result;
            }
            return null;
        }
    }
}
