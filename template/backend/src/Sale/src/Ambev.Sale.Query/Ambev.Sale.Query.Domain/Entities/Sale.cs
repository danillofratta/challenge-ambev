﻿using Ambev.Base.Domain.Entities;
using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Query.Domain.Entities;

public class Sale : BaseEntity
{
    /// <summary>
    /// It is generated by the database, but can be modified by the user later.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// External Identities
    /// </summary>
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// External Identities
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// External Identities
    /// </summary>
    public string BranchId { get; set; } = string.Empty;

    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// External Identities
    /// </summary>
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public SaleStatus Status { get; set; } = SaleStatus.NotCancelled;
    public List<SaleItem> SaleItens { get; set; } = new()!;    
}

