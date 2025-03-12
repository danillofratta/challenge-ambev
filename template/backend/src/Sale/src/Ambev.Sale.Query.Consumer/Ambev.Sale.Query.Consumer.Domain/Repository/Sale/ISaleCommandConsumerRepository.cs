using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository.Sale;

/// <summary>
/// Can only perform writing
/// </summary>
public interface ISaleCommandConsumerRepository : ICommandRepositoryBase<Query.Domain.Entities.Sale, Guid>
{

}

