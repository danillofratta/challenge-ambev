
using Ambev.Base.Domain.Entities;
using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Query.Domain.Entities;

/// <summary>
/// Represents item of sale
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// ID of Sale, an item is linked to only one sale
    /// </summary>
    public Guid SaleId { get; set; } 

    /// <summary>
    /// Object Sale
    /// </summary>
    public Sale Sale { get; set; } = null!;

    /// <summary>
    /// External Identities
    /// </summary>
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    /// External Identities
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of itens
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Price of item
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Discount applied as per rule
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Item total with discount applied
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Status: active or cancelled
    /// </summary>
    public SaleItemStatus Status { get; set; } = SaleItemStatus.NotCancelled;
}


