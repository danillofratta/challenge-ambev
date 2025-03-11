using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Domain.Repository;

namespace Ambev.Sale.Query.Infrastructure.Orm.Repository;

public class SaleItemQueryRepository : QueryRepositoryBase<Ambev.Sale.Query.Domain.Entities.SaleItem , Guid>, ISaleItemQueryRepository
{
    private readonly SaleQueryDbContext _SaleCommandDbContext;

    public SaleItemQueryRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleCommandDbContext = defaultDbContext;    
    }
}
