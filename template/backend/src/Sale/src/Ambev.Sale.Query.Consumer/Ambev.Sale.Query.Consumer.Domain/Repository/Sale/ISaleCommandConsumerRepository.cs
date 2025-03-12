using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository.Sale;

public interface ISaleCommandConsumerRepository : ICommandRepositoryBase<Query.Domain.Entities.Sale, Guid>
{

}

