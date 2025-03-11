using AutoMapper;

namespace Ambev.Sale.Query.Application.Sale.GetListPaginated
{
    public class GetSaleListPaginatedQueryProfile : Profile
    {
        public GetSaleListPaginatedQueryProfile()
        {
            CreateMap<Domain.Entities.Sale, GetSaleListPaginatedQueryResult>();
        }
    }
}
