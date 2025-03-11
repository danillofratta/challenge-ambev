using Ambev.Base.Domain.Repository;
using Ambev.Sale.Core.Domain;

namespace Ambev.Sale.Core.Domain.Repository;

public interface ISaleQueryRepository : IQueryRepositoryBase<Ambev.Sale.Command.Domain.Entities.Sale, Guid>
{

}

