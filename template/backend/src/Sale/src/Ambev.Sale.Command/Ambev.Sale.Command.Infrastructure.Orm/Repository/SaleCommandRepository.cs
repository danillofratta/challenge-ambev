
using Ambev.Base.Infrastructure.Command.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;

namespace Ambev.Sale.Command.Infrastructure.Orm.Repository;

public class SaleCommandRepository : CommandRepositoryBase<Ambev.Sale.Command.Domain.Entities.Sale, Guid>, ISaleCommandRepository
{
    private readonly SaleCommandDbContext _SaleCommandDbContext;

    public SaleCommandRepository(SaleCommandDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleCommandDbContext = defaultDbContext;
    }   
}

