
using Ambev.Sale.Core.Domain.Service;
using Ambev.Sale.Command.Domain.Entities;

namespace Ambev.Sale.Command.Tests.Unit.Domain.Service;

public class SaleDiscountServiceTests
{
    private readonly SaleDiscountService _service;

    public SaleDiscountServiceTests()
    {
        _service = new SaleDiscountService();
    }

    [Theory]
    [InlineData(3, 10, 0)]        // Menos de 4 itens = sem desconto
    [InlineData(4, 10, 4)]        // 4 itens = 10% desconto
    [InlineData(9, 10, 9)]        // 9 itens = 10% desconto
    [InlineData(10, 10, 20)]      // 10 itens = 20% desconto
    [InlineData(20, 10, 40)]      // 20 itens = 20% desconto
    public void CalculateItemDiscount_ValidQuantities_ReturnsCorrectDiscount(int quantity, decimal unitPrice, decimal expectedDiscount)
    {
        // Act
        var discount = _service.CalculateItemDiscount(quantity, unitPrice);

        // Assert
        Assert.Equal(expectedDiscount, discount);
        Assert.True(_service.IsValid);
    }

    [Fact]
    public void CalculateItemDiscount_QuantityAboveMax_ThrowsException()
    {
        // Arrange
        int quantity = 21;
        decimal unitPrice = 10m;

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            _service.CalculateItemDiscount(quantity, unitPrice));

        Assert.Equal("Cannot sell more than 20 identical items", exception.Message);
        Assert.False(_service.IsValid);
    }

    [Theory]
    [InlineData(1, true)]    // Quantidade válida
    [InlineData(20, true)]   // Limite máximo válido
    [InlineData(0, true)]    // Zero é válido
    public void ValidateQuantity_ValidQuantities_MaintainsIsValidTrue(int quantity, bool expectedIsValid)
    {
        // Act
        _service.ValidateQuantity(quantity);

        // Assert
        Assert.Equal(expectedIsValid, _service.IsValid);
    }

    [Fact]
    public void ValidateSaleItems_ValidItems_AppliesCorrectDiscounts()
    {
        // Arrange
        var items = new List<SaleItem>
            {
                new SaleItem { ProductId = Guid.NewGuid().ToString(), Quantity = 3, UnitPrice = 10m },    // Sem desconto
                new SaleItem { ProductId = Guid.NewGuid().ToString(), Quantity = 4, UnitPrice = 10m },    // 10% desconto
                new SaleItem { ProductId = Guid.NewGuid().ToString(), Quantity = 10, UnitPrice = 10m }    // 20% desconto
            };

        // Act
        _service.ValidateSaleItems(items);

        // Assert
        var itemNoDiscount = items[0];
        var itemBasicDiscount = items[1];
        var itemBulkDiscount = items[2];

        Assert.Equal(0m, itemNoDiscount.Discount);
        Assert.Equal(30m, itemNoDiscount.TotalPrice);

        Assert.Equal(4m, itemBasicDiscount.Discount);
        Assert.Equal(36m, itemBasicDiscount.TotalPrice);

        Assert.Equal(20m, itemBulkDiscount.Discount);
        Assert.Equal(80m, itemBulkDiscount.TotalPrice);

        Assert.True(_service.IsValid);
    }

    [Fact]
    public void ValidateSaleItems_SameProductExceedsMax_ThrowsException()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var items = new List<SaleItem>
            {
                new SaleItem { ProductId = productId, Quantity = 15, UnitPrice = 10m },
                new SaleItem { ProductId = productId, Quantity = 10, UnitPrice = 10m }  // Total 25 > 20
            };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            _service.ValidateSaleItems(items));

        Assert.Equal("Cannot sell more than 20 identical items", exception.Message);
        Assert.False(_service.IsValid);
    }

    [Fact]
    public void ValidateSaleItems_MultipleValidItemsSameProduct_AppliesDiscounts()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var items = new List<SaleItem>
            {
                new SaleItem { ProductId = productId, Quantity = 5, UnitPrice = 10m },
                new SaleItem { ProductId = productId, Quantity = 5, UnitPrice = 10m }  // Total 10
            };

        // Act
        _service.ValidateSaleItems(items);

        // Assert
        Assert.All(items, item =>
        {
            Assert.Equal(5m, item.Discount);  // Cada item tem 10 * 5 * 0.20 = 10 de desconto (20% por total >= 10)
            Assert.Equal(45m, item.TotalPrice);
        });
        Assert.True(_service.IsValid);
    }
}

