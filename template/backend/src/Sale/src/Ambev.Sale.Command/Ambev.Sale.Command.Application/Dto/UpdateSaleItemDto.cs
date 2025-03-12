using Ambev.Sale.Command.Domain.Enum;


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
