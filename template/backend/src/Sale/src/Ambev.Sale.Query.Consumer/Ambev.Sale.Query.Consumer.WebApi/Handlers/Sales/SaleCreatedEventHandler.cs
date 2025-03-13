
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;
using Rebus.Threading;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    /// <summary>
    /// Consumer to create sale in database read
    /// </summary>
    public class SaleCreatedConsumerEventHandler : Rebus.Handlers.IHandleMessages<SaleCreatedEvent>
    {
        private readonly ISaleCommandConsumerRepository _repository;
        private readonly ILogger<SaleCreatedConsumerEventHandler> _logger;

        public SaleCreatedConsumerEventHandler(ISaleCommandConsumerRepository repository, ILogger<SaleCreatedConsumerEventHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(SaleCreatedEvent message)
        {
            _logger.LogInformation("Start Process SaleCreatedConsumerEventHandler for SaleId {Id}", message.Id);
            try
            {
                var sale = new Ambev.Sale.Query.Domain.Entities.Sale
                {
                    Id = message.Id,
                    Number = message.Number,
                    CustomerId = message.CustomerId,
                    CustomerName = message.CustomerName,
                    BranchId = message.BranchId,
                    BranchName = message.BranchName,
                    TotalAmount = message.TotalAmount,
                    Status = (SaleStatus)message.Status,
                    SaleItens = message.SaleItens.Select(i => new Ambev.Sale.Query.Domain.Entities.SaleItem
                    {
                        Id = i.Id,
                        SaleId = i.SaleId,
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Discount = i.Discount,
                        TotalPrice = i.TotalPrice,
                        Status = (SaleItemStatus)i.Status
                    }).ToList()
                };

                await _repository.SaveAsync(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail Process SaleCreatedConsumerEventHandler for SaleId {Id}", message.Id);
                throw;
            }
            _logger.LogInformation("Success Process SaleCreatedConsumerEventHandler for SaleId {Id}", message.Id);
        }
    }
}
