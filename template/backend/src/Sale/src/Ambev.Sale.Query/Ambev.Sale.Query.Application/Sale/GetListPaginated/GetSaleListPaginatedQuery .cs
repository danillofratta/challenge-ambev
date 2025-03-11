using Ambev.Base.WebApi;
using MediatR;

namespace Ambev.Sale.Query.Application.Sale.GetListPaginated;

public class GetSaleListPaginatedQuery : IRequest<PaginatedList<GetSaleListPaginatedQueryResult>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetSaleListPaginatedQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}