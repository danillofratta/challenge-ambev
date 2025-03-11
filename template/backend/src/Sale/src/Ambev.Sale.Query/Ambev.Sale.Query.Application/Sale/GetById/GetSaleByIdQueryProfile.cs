using AutoMapper;

namespace Ambev.Sale.Query.Application.Sale.GetById
{
    public class GetSaleByIdQueryProfile : Profile
    {
        public GetSaleByIdQueryProfile()
        {
            CreateMap<Domain.Entities.Sale, GetSaleByIdQueryResult>();
        }
    }
}
