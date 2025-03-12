using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;

/// <summary>
/// Can only perform writing
/// </summary>
public interface ISaleItemCommandConsumerRepository : ICommandRepositoryBase<Query.Domain.Entities.SaleItem, Guid>
{

}

