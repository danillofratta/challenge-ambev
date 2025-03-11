
using Ambev.Sale.Command.Domain.Entities;

namespace Ambev.Sale.Core.Domain.Service
{
    /// <summary>
    /// Implements the business rule
    /// These business rules define quantity-based discounting tiers and limitations:
    /// 1. Discount Tiers: 
    ///- 4+ items: 10% discount
    ///- 10-20 items: 20% discount
    ///2. Restrictions: 
    ///- Maximum limit: 20 items per product
    ///- No discounts allowed for quantities below 4 items[
    /// </summary>
    public class SaleDiscountService
    {
        private const int MIN_DISCOUNT_QUANTITY = 4;
        private const int MID_DISCOUNT_QUANTITY = 10;
        private const int MAX_QUANTITY = 20;
        private const decimal BASIC_DISCOUNT = 0.10m;
        private const decimal BULK_DISCOUNT = 0.20m;
        public bool IsValid = true;

        /// <summary>
        /// Calculate discount according to rule
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="unitPrice"></param>
        /// <returns></returns>
        public decimal CalculateItemDiscount(int quantity, decimal unitPrice)
        {
            ValidateQuantity(quantity);

            if (quantity < MIN_DISCOUNT_QUANTITY)
                return 0;

            var totalPrice = quantity * unitPrice;
            var discountPercentage = quantity >= MID_DISCOUNT_QUANTITY ? BULK_DISCOUNT : BASIC_DISCOUNT;

            return totalPrice * discountPercentage;
        }

        /// <summary>
        /// Validates if there are more than 20 items of the product
        /// </summary>
        /// <param name="quantity"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ValidateQuantity(int quantity)
        {
            if (quantity > MAX_QUANTITY)
            {
                this.IsValid = false;                
                throw new InvalidOperationException($"Cannot sell more than {MAX_QUANTITY} identical items");
            }
        }

        /// <summary>
        /// Valid all sale itens
        /// </summary>
        /// <param name="items"></param>
        public void ValidateSaleItems(IEnumerable<SaleItem> items)
        {
            var groupedItems = items.GroupBy(x => x.ProductId);

            foreach (var group in groupedItems)
            {
                var totalQuantity = group.Sum(x => x.Quantity);
                ValidateQuantity(totalQuantity);

                foreach (var item in group)
                {
                    var discount = CalculateItemDiscount(item.Quantity, item.UnitPrice);
                    item.Discount = discount;
                    item.TotalPrice = (item.Quantity * item.UnitPrice) - discount;
                }
            }
        }
    }
}
