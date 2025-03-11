using AutoMapper;

namespace Ambev.Sale.Query.Application.SaleItem.GetById
{
    public class GetSaleItemByIdQueryProfile : Profile
    {
        public GetSaleItemByIdQueryProfile()
        {
            CreateMap<Domain.Entities.Sale, GetSaleItemByIdQueryResult>();
        }
    }
}
