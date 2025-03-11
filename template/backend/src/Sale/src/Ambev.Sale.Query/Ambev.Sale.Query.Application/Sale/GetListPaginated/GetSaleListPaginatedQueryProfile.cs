using Ambev.Sale.Query.Application.Dto;
using AutoMapper;

namespace Ambev.Sale.Query.Application.Sale.GetListPaginated
{
    public class GetSaleListPaginatedQueryProfile : Profile
    {
        public GetSaleListPaginatedQueryProfile()
        {
            CreateMap<SaleItemDto, Domain.Entities.SaleItem>();
            CreateMap<Domain.Entities.SaleItem, SaleItemDto>();

            CreateMap<GetSaleListPaginatedQueryResult, Domain.Entities.Sale>()
                .ForMember(dto => dto.SaleItens, conf => conf.MapFrom(ol => ol.SaleItens));

            CreateMap<Domain.Entities.Sale, GetSaleListPaginatedQueryResult>()
                .ForMember(dto => dto.SaleItens, conf => conf.MapFrom(ol => ol.SaleItens));

            
        }
    }
}
