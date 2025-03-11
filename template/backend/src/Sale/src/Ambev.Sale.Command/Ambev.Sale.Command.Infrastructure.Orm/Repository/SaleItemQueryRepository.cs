using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;

namespace Ambev.Sale.Command.Infrastructure.Orm.Repository;

public class SaleItemQueryRepository : QueryRepositoryBase<Ambev.Sale.Command.Domain.Entities.SaleItem , Guid>, ISaleItemQueryRepository
{
    private readonly SaleCommandDbContext _SaleCommandDbContext;

    public SaleItemQueryRepository(SaleCommandDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleCommandDbContext = defaultDbContext;    
    }
}
