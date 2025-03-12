using Ambev.Base.Domain.Specification;
using Ambev.Sale.Command.Domain.Entities;
using Ambev.Sale.Command.Domain.Enum;

namespace Ambev.Sale.Command.Domain.Specification
{
    public class SaleActiveSpecification : BaseSpecification<Sale.Command.Domain.Entities.Sale>
    {
        public override bool IsSatisfiedBy(Entities.Sale entity)
        {
            return entity.Status == SaleStatus.NotCancelled;
        }
    }
}