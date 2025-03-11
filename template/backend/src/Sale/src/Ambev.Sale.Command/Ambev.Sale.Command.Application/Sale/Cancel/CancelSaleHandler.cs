﻿using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Core.Domain.Repository;
using Ambev.Base.Infrastructure.Messaging;
using Ambev.Sale.Command.Application.Sale.Create;


namespace Ambev.Sale.Command.Application.Sale.Cancel
{
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
            record.Status = Domain.Enum.SaleStatus.Cancelled;

            var update = await _repositorycommand.UpdateAsync(record);

            //publich event 
            await _mediator.Publish(new CreateSaleResult
            {
                Id = update.Id
            });
            await Task.FromResult("Sale Cancelled");

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

            return _mapper.Map<CancelSaleResult>(update); ;
        }
    }
}
