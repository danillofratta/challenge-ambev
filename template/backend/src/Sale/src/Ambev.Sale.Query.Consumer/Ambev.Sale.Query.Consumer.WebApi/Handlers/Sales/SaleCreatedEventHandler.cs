
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Domain.Enum;
using Rebus.Handlers;
using Rebus.Threading;

namespace Ambev.Sale.Query.Consumer.WebApi.Sales
{
    public class SaleCreatedEventHandler : Rebus.Handlers.IHandleMessages<SaleCreatedEvent>
    {
        private readonly ISaleCommandConsumerRepository _repository;

        public SaleCreatedEventHandler(ISaleCommandConsumerRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SaleCreatedEvent message)
        {
            // Criar a entidade baseada no evento recebido
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

            // Salvar no banco de dados
            await _repository.SaveAsync(sale);
        }
    }
}
