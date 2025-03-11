using MediatR;

namespace Ambev.Sale.Query.Application.Sale.GetById
{
    public class GetSaleByIdQuery : IRequest<GetSaleByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
