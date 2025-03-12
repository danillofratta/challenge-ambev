using AutoMapper;

namespace Ambev.Sale.Query.Application.Sale.GetItensOfSale
{
    public class GetItensOfSaleQueryProfile : Profile
    {
        public GetItensOfSaleQueryProfile()
        {
            CreateMap<Domain.Entities.SaleItem, GetItensOfSaleQueryResult>();
        }
    }
}
