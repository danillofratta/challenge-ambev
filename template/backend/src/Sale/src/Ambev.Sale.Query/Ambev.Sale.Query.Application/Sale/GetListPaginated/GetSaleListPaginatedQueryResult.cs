using Ambev.Sale.Query.Application.Dto;
using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Query.Application.Sale.GetListPaginated;

public class GetSaleListPaginatedQueryResult
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string BranchId { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public SaleStatus Status { get; set; }
    public List<SaleItemDto> SaleItens { get; set; }
}

