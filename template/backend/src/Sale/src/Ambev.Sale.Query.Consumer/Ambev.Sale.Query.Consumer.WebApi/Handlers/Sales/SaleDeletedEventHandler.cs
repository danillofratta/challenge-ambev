
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Rebus.Handlers;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    /// <summary>
    /// Consumer to delete sale in database read
    /// </summary>
    public class SaleDeletedConsumerEventHandler : Rebus.Handlers.IHandleMessages<SaleDeletedEvent>
    {
        private readonly ISaleCommandConsumerRepository _repositorycommnad;
        private readonly ISaleQueryConsumerRepository _repositoryquery;
        private readonly ILogger<SaleDeletedConsumerEventHandler> _logger;

        public SaleDeletedConsumerEventHandler(ISaleCommandConsumerRepository repositorycommnad, ISaleQueryConsumerRepository repositoryquery, ILogger<SaleDeletedConsumerEventHandler> logger)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
            _logger = logger;
        }
        public async Task Handle(SaleDeletedEvent message)
        {
            _logger.LogInformation("Start Process SaleDeletedConsumerEventHandler for SaleId {Id}", message.Id);
            try
            {
                var sale = await _repositoryquery.GetByIdAsync(message.Id);

                if (sale != null)
                {
                    await _repositorycommnad.DeleteAsync(sale);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail Process SaleDeletedConsumerEventHandler for SaleId {Id}", message.Id);
                throw;
            }
            _logger.LogInformation("Success Process SaleDeletedConsumerEventHandler for SaleId {Id}", message.Id);
        }
    }
}
