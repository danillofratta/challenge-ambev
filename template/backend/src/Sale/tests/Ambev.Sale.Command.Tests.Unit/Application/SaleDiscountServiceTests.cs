using Ambev.Sale.Command.Domain.Entities;
using Ambev.Sale.Core.Domain.Service;

namespace Ambev.Tests.Unit.Application;


public class SaleDiscountServiceTests
{
    private readonly SaleDiscountService _service;

    public SaleDiscountServiceTests()
    {
        _service = new SaleDiscountService();
    }

    /// <summary>
    /// Test if you don't receive a discount 
    /// </summary>
    [Fact]
    public void CalculateItemDiscount_QuantityLessThanMin_ReturnsZeroDiscount()
    {
        // Arrange
        int quantity = 3; // Less than MIN_DISCOUNT_QUANTITY
        decimal unitPrice = 10m;

        // Act
        var discount = _service.CalculateItemDiscount(quantity, unitPrice);

        // Assert
        Assert.Equal(0, discount);
    }

    /// <summary>
    /// Test if receive 10% discount
    /// </summary>
    [Fact]
    public void CalculateItemDiscount_QuantityBetweenMinAndMid_ReturnsBasicDiscount()
    {
        // Arrange
        int quantity = 5; // Between MIN_DISCOUNT_QUANTITY and MID_DISCOUNT_QUANTITY
        decimal unitPrice = 10m;

        // Act
        var discount = _service.CalculateItemDiscount(quantity, unitPrice);

        // Assert
        Assert.Equal(5, discount); // 5 items * 10 unit price * 10% discount
    }

    /// <summary>
    /// Test if receive 20% discount
    /// </summary>
    [Fact]
    public void CalculateItemDiscount_QuantityGreaterThanMid_ReturnsBulkDiscount()
    {
        // Arrange
        int quantity = 15; // Greater than MID_DISCOUNT_QUANTITY
        decimal unitPrice = 10m;

        // Act
        var discount = _service.CalculateItemDiscount(quantity, unitPrice);

        // Assert
        Assert.Equal(30, discount); // 15 items * 10 unit price * 20% discount
    }

    /// <summary>
    /// Test if more than 20 items do not sell
    /// </summary>
    [Fact]
    public void ValidateQuantity_QuantityGreaterThanMax_ThrowsInvalidOperationException()
    {
        // Arrange
        int quantity = 21; // Greater than MAX_QUANTITY

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.ValidateQuantity(quantity));
        Assert.Equal("Cannot sell more than 20 identical items", exception.Message);
    }

    /// <summary>
    /// Test the same duplicate product in the list 
    /// </summary>
    [Fact]
    public void ValidateSaleItems_ValidItems_CorrectlyAppliesDiscount()
    {
        // Arrange
        var items = new List<SaleItem>
        {
            new SaleItem { ProductId = "1", Quantity =5, UnitPrice = 10m },
            new SaleItem { ProductId = "1", Quantity = 5, UnitPrice = 10m }
        };

        // Act
        _service.ValidateSaleItems(items);

        foreach (var item in items)
        {
            _service.CalculateItemDiscount(item.Quantity, item.UnitPrice);
        }

        decimal totaldiscout = items.Sum(x => x.Discount);
       
        Assert.Equal(totaldiscout,10); 
    }

    /// <summary>
    /// Test the same duplicate product in the list, exceeds quantity limit 
    /// </summary>
    [Fact]
    public void ValidateSaleItems_QuantityExceedsMax_ThrowsException()
    {
        // Arrange
        var items = new List<SaleItem>
        {
            new SaleItem { ProductId = "1", Quantity = 10, UnitPrice = 10m },
            new SaleItem { ProductId = "1", Quantity = 15, UnitPrice = 10m }
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.ValidateSaleItems(items));
        Assert.Equal("Cannot sell more than 20 identical items", exception.Message);
    }
}
