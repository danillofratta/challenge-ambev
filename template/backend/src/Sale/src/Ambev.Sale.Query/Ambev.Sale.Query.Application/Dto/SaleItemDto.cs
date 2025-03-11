using Ambev.Sale.Query.Domain.Enum;


namespace Ambev.Sale.Query.Application.Dto;
public record SaleItemDto
(
    string ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal TotalPrice,
    SaleStatus Status = SaleStatus.NotCancelled
);
