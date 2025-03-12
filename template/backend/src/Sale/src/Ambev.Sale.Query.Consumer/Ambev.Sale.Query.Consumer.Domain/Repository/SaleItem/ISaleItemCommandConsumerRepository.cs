using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;

public interface ISaleItemCommandConsumerRepository : ICommandRepositoryBase<Query.Domain.Entities.SaleItem, Guid>
{

}

