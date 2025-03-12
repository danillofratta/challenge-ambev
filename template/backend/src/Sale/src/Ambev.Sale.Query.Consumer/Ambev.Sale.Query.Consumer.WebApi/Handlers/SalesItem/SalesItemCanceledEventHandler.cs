
using Ambev.Sale.Contracts.Events.SaleItem;
using Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi.SalesItem
{
    /// <summary>
    /// Consumer to cancel item in database read
    /// </summary>
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
            var saleitem = await _repositoryquery.GetByIdAsync(message.Id);

            if (saleitem != null)
            {
                saleitem.Status = SaleItemStatus.Cancelled;                

                await _repositorycommnad.UpdateAsync(saleitem);
            }
        }
    }
}

