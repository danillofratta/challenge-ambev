using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository.Sale;

/// <summary>
/// You can only make inquiries,
/// </summary>
public interface ISaleQueryConsumerRepository : IQueryRepositoryBase<Query.Domain.Entities.Sale, Guid>
{

}

