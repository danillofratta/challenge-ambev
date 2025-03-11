using MediatR;

namespace Ambev.Sale.Command.Application.Sale.Cancel
{
    public class CancelSaleCommand : IRequest<CancelSaleResult>
    {
        public Guid id { get; set; }
    }
}
