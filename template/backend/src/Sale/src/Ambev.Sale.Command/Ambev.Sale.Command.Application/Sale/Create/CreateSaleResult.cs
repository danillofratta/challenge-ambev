using MediatR;

namespace Ambev.Sale.Command.Application.Sale.Create
{
    public class CreateSaleResult : INotification
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
    }
}
