using MediatR;

namespace Ambev.Sale.Query.Application.SaleItem.GetById
{
    public class GetSaleItemByIdQuery : IRequest<GetSaleItemByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
