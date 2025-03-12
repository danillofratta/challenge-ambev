using Ambev.Sale.Command.Application.Dto;
using Ambev.Sale.Contracts.Dto;
using AutoMapper;

namespace Ambev.Sale.Command.Application.Sale.Create
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {

            CreateMap<CreateSaleCommand, Domain.Entities.Sale>()
                   .ForMember(dto => dto.SaleItens, conf => conf.MapFrom(ol => ol.SaleItens));

            CreateMap<CreateSaleItemDto, Domain.Entities.SaleItem>();

            CreateMap<Domain.Entities.Sale, CreateSaleResult>();

            CreateMap<Domain.Entities.SaleItem, SaleItemDto>();
        }
    }
}
