using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;

/// <summary>
/// You can only make inquiries,
/// </summary>
public interface ISaleItemQueryConsumerRepository : IQueryRepositoryBase<Ambev.Sale.Query.Domain.Entities.SaleItem, Guid>
{

}

