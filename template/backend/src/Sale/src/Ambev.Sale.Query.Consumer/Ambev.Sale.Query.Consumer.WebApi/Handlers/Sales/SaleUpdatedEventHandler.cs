
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;
using Rebus.Threading;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    public class SaleUpdatedEventHandler : Rebus.Handlers.IHandleMessages<SaleUpdatedEvent>
    {
        private readonly ISaleCommandConsumerRepository _repositorycommnad;
        private readonly ISaleQueryConsumerRepository _repositoryquery;

        public SaleUpdatedEventHandler(ISaleCommandConsumerRepository repositorycommnad, ISaleQueryConsumerRepository repositoryquery)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
        }

        public async Task Handle(SaleUpdatedEvent message)
        {
            var sale = await _repositoryquery.GetByIdAsync(message.Id);

            if (sale != null)
            {
                sale.Number = message.Number;
                sale.CustomerId = message.CustomerId;
                sale.CustomerName = message.CustomerName;
                sale.BranchId = message.BranchId;
                sale.BranchName = message.BranchName;
                sale.TotalAmount = message.TotalAmount;
                sale.Status = (SaleStatus)message.Status;

                await _repositorycommnad.UpdateAsync(sale);
            }
        }
    }
}