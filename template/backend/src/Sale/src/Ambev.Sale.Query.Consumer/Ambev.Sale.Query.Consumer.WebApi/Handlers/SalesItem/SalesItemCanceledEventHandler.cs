
using Ambev.Sale.Contracts.Events.SaleItem;
using Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi.SalesItem
{
    public class SaleItemCanceledEventHandler : Rebus.Handlers.IHandleMessages<SaleItemCanceledEvent>
    {
        private readonly ISaleItemCommandConsumerRepository _repositorycommnad;
        private readonly ISaleItemQueryConsumerRepository _repositoryquery;

        public SaleItemCanceledEventHandler(ISaleItemCommandConsumerRepository repositorycommnad, ISaleItemQueryConsumerRepository repositoryquery)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
        }

        public async Task Handle(SaleItemCanceledEvent message)
        {
            var sale = await _repositoryquery.GetByIdAsync(message.Id);

            if (sale != null)
            {
                sale.Status = SaleItemStatus.Cancelled;

                await _repositorycommnad.UpdateAsync(sale);
            }
        }
    }
}

