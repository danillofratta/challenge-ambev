using Ambev.Sale.Command.Application.Dto;
using AutoMapper;

namespace Ambev.Sale.Command.Application.Sale.Update
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {

            CreateMap<UpdateSaleCommand, Domain.Entities.Sale>();
            CreateMap<UpdateSaleItemDto, Domain.Entities.SaleItem>();
            CreateMap<Domain.Entities.Sale, UpdateSaleResult>();

            CreateMap<UpdateSaleCommand, Domain.Entities.Sale>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
        }
    }
}
