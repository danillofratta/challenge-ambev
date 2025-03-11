using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository;

public interface ISaleCommandConsumerRepository : ICommandRepositoryBase<Ambev.Sale.Query.Domain.Entities.Sale, Guid>
{

}

