using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Core.Domain.Repository;

public interface ISaleItemQueryRepository : IQueryRepositoryBase<Ambev.Sale.Command.Domain.Entities.SaleItem, Guid>
{

}

