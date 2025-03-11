using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository;

public interface ISaleQueryConsumerRepository : IQueryRepositoryBase<Ambev.Sale.Query.Domain.Entities.Sale, Guid>
{

}

