using Ambev.Sale.Command.Domain.Enum;

namespace Ambev.Sale.Command.Application.Dto;
public record CreateSaleItemDto
(
    string ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal TotalPrice,
    SaleStatus Status = SaleStatus.NotCancelled
);
