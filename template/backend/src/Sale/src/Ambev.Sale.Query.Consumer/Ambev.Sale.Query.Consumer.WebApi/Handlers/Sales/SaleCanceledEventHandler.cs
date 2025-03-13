
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    /// <summary>
    /// Consumer to cancel sale in database read
    /// </summary>
    public class SaleCanceledConsumerEventHandler : Rebus.Handlers.IHandleMessages<SaleCanceledEvent>
    {
        private readonly ISaleCommandConsumerRepository _repositorycommnad;
        private readonly ISaleQueryConsumerRepository _repositoryquery;
        private readonly ILogger<SaleCanceledConsumerEventHandler> _logger;

        public SaleCanceledConsumerEventHandler(ILogger<SaleCanceledConsumerEventHandler> logger, ISaleCommandConsumerRepository repositorycommnad, ISaleQueryConsumerRepository repositoryquery)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
            _logger = logger;
        }

        public async Task Handle(SaleCanceledEvent message)
        {
            _logger.LogInformation("Start Process SaleCanceledConsumerEventHandler for SaleId {Id}", message.Id);
            try
            {
                var sale = await _repositoryquery.GetByIdAsync(message.Id);

                if (sale != null)
                {
                    sale.Cancel();

                    await _repositorycommnad.UpdateAsync(sale);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail Process SaleCanceledConsumerEventHandler for SaleId {Id}", message.Id);
                throw;
            }
            _logger.LogInformation("Success Process SaleCanceledConsumerEventHandler for SaleId {Id}", message.Id);
        }
    }
}

