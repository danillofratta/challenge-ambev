using Ambev.Sale.Query.Domain.Enum;


namespace Ambev.Sale.Query.Application.Dto;
public record SaleItemDto
(
    Guid Id,
    Guid SaleId,
    string ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal TotalPrice,
    SaleItemStatus Status = SaleItemStatus.NotCancelled
);
