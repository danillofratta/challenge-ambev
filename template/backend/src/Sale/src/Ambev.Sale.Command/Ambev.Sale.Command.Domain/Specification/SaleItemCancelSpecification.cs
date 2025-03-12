using Ambev.Base.Domain.Specification;
using Ambev.Sale.Command.Domain.Entities;
using Ambev.Sale.Command.Domain.Enum;

namespace Ambev.Sale.Command.Domain.Specification
{
    /// <summary>
    /// Check sale item is cancel
    /// </summary>
    public class SaleItemCancelSpecification : BaseSpecification<Sale.Command.Domain.Entities.SaleItem>
    {
        public override bool IsSatisfiedBy(Entities.SaleItem entity)
        {
            return entity.Status == SaleItemStatus.Cancelled;
        }
    }
}