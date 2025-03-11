using MediatR;

namespace Ambev.Sale.Command.Application.SaleItem.Cancel
{
    public class CancelSaleItemResult : INotification
    {
        public Guid id { get; set; }
    }
}
