
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    /// <summary>
    /// Consumer to delete sale in database read
    /// </summary>
    public class SaleDeletedEventHandler : Rebus.Handlers.IHandleMessages<SaleDeletedEvent>
    {
        private readonly ISaleCommandConsumerRepository _repositorycommnad;
        private readonly ISaleQueryConsumerRepository _repositoryquery;

        public SaleDeletedEventHandler(ISaleCommandConsumerRepository repositorycommnad, ISaleQueryConsumerRepository repositoryquery)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
        }
        public async Task Handle(SaleDeletedEvent message)
        {
            var sale = await _repositoryquery.GetByIdAsync(message.Id);

            if (sale != null)
            {
                await _repositorycommnad.DeleteAsync(sale);
            }
        }
    }
}
