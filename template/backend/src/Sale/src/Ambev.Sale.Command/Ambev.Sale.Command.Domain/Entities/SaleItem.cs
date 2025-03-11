
using Ambev.Base.Domain.Entities;
using Ambev.Sale.Command.Domain.Enum;

namespace Ambev.Sale.Command.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; } 
    public Sale Sale { get; set; } = null!;

    /// <summary>
    /// External Identities
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// External Identities
    /// </summary>
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
    public SaleItemStatus Status { get; set; } = SaleItemStatus.NotCancelled;
}


