using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Domain.Repository;

namespace Ambev.Sale.Query.Infrastructure.Orm.Repository;

public class SaleQueryRepository : QueryRepositoryBase<Ambev.Sale.Query.Domain.Entities.Sale , Guid>, ISaleQueryRepository
{
    private readonly SaleQueryDbContext _SaleCommandDbContext;

    public SaleQueryRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleCommandDbContext = defaultDbContext;    
    }
}
