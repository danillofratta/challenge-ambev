using MediatR;

namespace Ambev.Sale.Query.Application.Sale.GetItensOfSale
{
    public class GetItensOfSaleQuery : IRequest<List<GetItensOfSaleQueryResult>>
    {
        public Guid Id { get; set; }
    }
}
