using Ambev.Sale.Query.Application.Dto;
using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Query.Application.Sale.GetItensOfSale;

public class GetItensOfSaleQueryResult
{
    public Guid Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
    public SaleItemStatus Status { get; set; } = SaleItemStatus.NotCancelled;
}

