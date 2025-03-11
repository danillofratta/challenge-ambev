using MediatR;

namespace Ambev.Sale.Command.Application.SaleItem.Cancel
{
    public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
    {
        public Guid id { get; set; }
    }
}
