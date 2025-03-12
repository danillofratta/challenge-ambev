using Ambev.Sale.Command.Domain.Entities;
using Ambev.Sale.Command.Domain.Enum;
using Ambev.Sale.Core.Domain.Service;


/// <summary>
/// Recalculates item discounts and total sales. 
/// Function called for example when canceling or adding an item to a sale
/// </summary>
public class SaleRecalculationService
{
    /// <summary>
    /// Reuse the rules to apply discounts
    /// </summary>
    private readonly SaleDiscountService _discountService;

    public SaleRecalculationService(SaleDiscountService discountService)
    {
        _discountService = discountService;
    }

    /// <summary>
    /// Recalculates the total sale
    /// </summary>
    /// <param name="sale"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void RecalculateSale(Sale sale)
    {
        if (sale == null)
            throw new InvalidOperationException("Sale cannot be null");

        var activeItems = sale.SaleItens
            .Where(x => x.Status != SaleItemStatus.Cancelled)
            .ToList();

        _discountService.ValidateSaleItems(activeItems);

        sale.TotalAmount = activeItems.Sum(x => x.TotalPrice);
        UpdateSaleStatus(sale, activeItems);
    }

    /// <summary>
    /// If all itens cancelled, then cancel sale
    /// </summary>
    /// <param name="sale"></param>
    private void UpdateSaleStatus(Sale sale, List<SaleItem> listitemactive)
    {
        if (listitemactive.Count > 0)
        {
            sale.Status = SaleStatus.NotCancelled;
        }
        else if (sale.Status != SaleStatus.Cancelled)
        {
            sale.Status = SaleStatus.Cancelled;
        }
    }
}
