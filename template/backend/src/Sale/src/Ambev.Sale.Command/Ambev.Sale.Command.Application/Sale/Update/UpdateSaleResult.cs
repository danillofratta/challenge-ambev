using MediatR;

namespace Ambev.Sale.Command.Application.Sale.Update
{
    public class UpdateSaleResult : INotification
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
    }
}
