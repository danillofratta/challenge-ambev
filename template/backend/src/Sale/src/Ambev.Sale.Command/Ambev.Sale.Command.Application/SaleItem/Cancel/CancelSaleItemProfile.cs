
using AutoMapper;

namespace Ambev.Sale.Command.Application.SaleItem.Cancel
{
    public class CancelSaleItemProfile : Profile
    {
        public CancelSaleItemProfile()
        {       
            CreateMap<CancelSaleItemCommand, Ambev.Sale.Command.Domain.Entities.SaleItem>();
            CreateMap<Ambev.Sale.Command.Domain.Entities.SaleItem, CancelSaleItemResult>();            
        }
    }
}
