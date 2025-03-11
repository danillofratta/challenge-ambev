using Ambev.Sale.Command.Application.Dto;
using Ambev.Sale.Command.Domain.Enum;
using MediatR;

namespace Ambev.Sale.Command.Application.Sale.Update
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid id { get; set; }
        public int Number { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public SaleStatus Status { get; set; }
        public List<UpdateSaleItemDto> SaleItens { get; set; } = new();
    }
}
