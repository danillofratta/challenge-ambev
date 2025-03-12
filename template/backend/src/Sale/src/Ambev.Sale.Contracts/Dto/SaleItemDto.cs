using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Contracts.Dto
{
    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SaleId { get; set; }       
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public SaleItemStatusDto Status { get; set; } 
    }
}
