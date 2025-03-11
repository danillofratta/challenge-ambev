﻿
using Ambev.Sale.Contracts.Dto;
using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Contracts.Events
{
    public class SaleUpdateEvent
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// It is generated by the database, but can be modified by the user later.
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
        public string BranchId { get; set; }

        /// <summary>
        /// External Identities
        /// </summary>
        public string BranchName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public SaleStatusDto Status { get; set; }
        public List<SaleItemDto> SaleItens { get; set; } = new()!;
    }
}
