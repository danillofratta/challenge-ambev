using AutoMapper;

namespace Ambev.Sale.Command.Application.Sale.Delete
{
    public class DeleteSaleProfile : Profile
    {
        public DeleteSaleProfile()
        {
            CreateMap<DeleteSaleCommand, Domain.Entities.Sale>();
            CreateMap<Domain.Entities.Sale, DeleteSaleResult>();
        }
    }
}
