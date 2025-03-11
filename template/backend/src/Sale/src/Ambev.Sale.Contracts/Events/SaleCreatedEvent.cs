using Ambev.Sale.Contracts.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Ambev.Sale.Contracts.Events
{
    public class SaleCreatedEvent
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Number { get; set; }

        /// <summary>
        /// External Identities
        /// </summary>
        public string CustomerId { get; set; }

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
        public string Status { get; set; }
        public List<SaleItemDto> SaleItens { get; set; } = new()!;
    }
}
