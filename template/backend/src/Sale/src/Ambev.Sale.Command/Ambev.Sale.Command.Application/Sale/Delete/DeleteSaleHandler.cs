using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;

namespace Ambev.Sale.Command.Application.Sale.Delete
{
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
            record.Status = Domain.Enum.SaleStatus.Cancelled;

            var update = await _repositorycommand.DeleteAsync(record);

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

            return _mapper.Map<DeleteSaleResult>(update);
        }
    }
}
