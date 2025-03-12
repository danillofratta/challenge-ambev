using Ambev.Sale.Contracts.Dto;
using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Contracts.Events
{
    /// <summary>
    /// Event responsável por sincronizar bando de dado write e read
    /// </summary>
    public class SaleUpdatedEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Number { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public SaleStatusDto Status { get; set; }
        public List<SaleItemDto> SaleItens { get; set; } = new()!;
    }
}
