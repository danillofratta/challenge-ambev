using MediatR;

namespace Ambev.Sale.Command.Application.Sale.Delete
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        public Guid Id { get; set; }
    }
}
