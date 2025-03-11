﻿
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi
{
    public class SaleCanceledEventHandler : Rebus.Handlers.IHandleMessages<SaleCanceledEvent>
    {
        private readonly ISaleCommandConsumerRepository _repositorycommnad;
        private readonly ISaleQueryConsumerRepository _repositoryquery;

        public SaleCanceledEventHandler(ISaleCommandConsumerRepository repositorycommnad, ISaleQueryConsumerRepository repositoryquery)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
        }

        public async Task Handle(SaleCanceledEvent message)
        {
            var sale = await _repositoryquery.GetByIdAsync(message.Id);

            if (sale != null)
            {
                sale.Status = SaleStatus.Cancelled;

                await _repositorycommnad.UpdateAsync(sale);
            }
        }
    }
}

