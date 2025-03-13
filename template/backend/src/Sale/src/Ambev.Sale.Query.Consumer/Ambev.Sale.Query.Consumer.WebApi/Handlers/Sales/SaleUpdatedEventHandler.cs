
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;
using Rebus.Threading;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    /// <summary>
    /// Consumer to update sale in database read
    /// </summary>
    public class SaleUpdatedConsumerEventHandler : Rebus.Handlers.IHandleMessages<SaleUpdatedEvent>
    {
        private readonly ISaleCommandConsumerRepository _repositorycommnad;
        private readonly ISaleQueryConsumerRepository _repositoryquery;
        private readonly ILogger<SaleUpdatedConsumerEventHandler> _logger;

        public SaleUpdatedConsumerEventHandler(ISaleCommandConsumerRepository repositorycommnad, ISaleQueryConsumerRepository repositoryquery, ILogger<SaleUpdatedConsumerEventHandler> logger)
        {
            _repositorycommnad = repositorycommnad;
            _repositoryquery = repositoryquery;
            _logger = logger;
        }

        public async Task Handle(SaleUpdatedEvent message)
        {
            _logger.LogInformation("Start Process SaleUpdatedConsumerEventHandler for SaleId {Id}", message.Id);
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail Process SaleUpdatedConsumerEventHandler for SaleId {Id}", message.Id);
                throw;
            }
            _logger.LogInformation("Success Process SaleUpdatedConsumerEventHandler for SaleId {Id}", message.Id);
        }
    }
}