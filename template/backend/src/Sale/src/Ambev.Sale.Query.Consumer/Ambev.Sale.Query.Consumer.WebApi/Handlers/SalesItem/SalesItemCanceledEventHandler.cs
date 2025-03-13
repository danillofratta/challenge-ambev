
using Ambev.Sale.Contracts.Events.SaleItem;
using Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;
using Ambev.Sale.Query.Consumer.WebApi.Sales;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi.SalesItem
{
    /// <summary>
    /// Consumer to cancel item in database read
    /// </summary>
    public class SaleItemCanceledConsumerEventHandler : Rebus.Handlers.IHandleMessages<SaleItemCanceledEvent>
    {
        private readonly ISaleItemCommandConsumerRepository _repositorycommnad;
        private readonly ISaleItemQueryConsumerRepository _repositoryquery;
        private readonly ILogger<SaleItemCanceledConsumerEventHandler> _logger;

        public SaleItemCanceledConsumerEventHandler(ISaleItemCommandConsumerRepository repositorycommnad, ISaleItemQueryConsumerRepository repositoryquery, ILogger<SaleItemCanceledConsumerEventHandler> logger)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
            _logger = logger;
        }

        public async Task Handle(SaleItemCanceledEvent message)
        {
            _logger.LogInformation("Start Process SaleItemCanceledConsumerEventHandler for SaleId {Id}", message.Id);
            try
            {
                var saleitem = await _repositoryquery.GetByIdAsync(message.Id);

                if (saleitem != null)
                {
                    saleitem.Status = SaleItemStatus.Cancelled;

                    await _repositorycommnad.UpdateAsync(saleitem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail Process SaleItemCanceledConsumerEventHandler for SaleId {Id}", message.Id);
                throw;
            }
            _logger.LogInformation("Success Process SaleItemCanceledConsumerEventHandler for SaleId {Id}", message.Id);
        }
    }
}

