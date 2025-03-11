using Ambev.Base.Domain.Repository;

namespace Ambev.Sale.Core.Domain.Repository;
public interface ISaleCommandRepository : ICommandRepositoryBase<Ambev.Sale.Command.Domain.Entities.Sale, Guid>
{

}

