using Ambev.Sale.Query.Domain.Enum;

namespace Ambev.Sale.Command.Application.Dto
{
    public record UpdateSaleItemDto
    (
        Guid SaleId,
        Guid Id,
        string ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal Discount,
        decimal TotalPrice,
        SaleStatus Status
    );
}
