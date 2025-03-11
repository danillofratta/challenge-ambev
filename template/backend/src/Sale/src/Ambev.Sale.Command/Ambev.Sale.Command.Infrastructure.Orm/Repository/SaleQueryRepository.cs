using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;

namespace Ambev.Sale.Command.Infrastructure.Orm.Repository;

public class SaleQueryRepository : QueryRepositoryBase<Ambev.Sale.Command.Domain.Entities.Sale , Guid>, ISaleQueryRepository
{
    private readonly SaleCommandDbContext _SaleCommandDbContext;

    public SaleQueryRepository(SaleCommandDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleCommandDbContext = defaultDbContext;    
    }
}
