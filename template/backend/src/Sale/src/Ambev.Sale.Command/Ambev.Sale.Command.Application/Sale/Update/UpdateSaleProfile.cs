using Ambev.Sale.Command.Application.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sale.Command.Application.Sale.Update
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {

            CreateMap<UpdateSaleCommand, Domain.Entities.Sale>()
                   .ForMember(dto => dto.SaleItens, conf => conf.MapFrom(ol => ol.SaleItens));

            CreateMap<UpdateSaleItemDto, Domain.Entities.SaleItem>();
            CreateMap<Domain.Entities.Sale, UpdateSaleResult>();
        }
    }
}
