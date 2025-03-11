using AutoMapper;
namespace Ambev.Sale.Command.Application.Sale.Cancel
{
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleCommand, Ambev.Sale.Command.Domain.Entities.Sale>();
            CreateMap<Ambev.Sale.Command.Domain.Entities.Sale, CancelSaleResult>();
        }
    }
}
