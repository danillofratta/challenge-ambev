using Ambev.Sale.Command.Application.Dto;
using Ambev.Sale.Command.Domain.Enum;
using MediatR;

namespace Ambev.Sale.Command.Application.Sale.Create
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string BranchId { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public SaleStatus Status { get; set; } = SaleStatus.NotCancelled;
        public List<CreateSaleItemDto> SaleItens { get; set; } = new();
    }
}
