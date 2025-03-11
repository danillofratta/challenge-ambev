
using Ambev.Base.Infrastructure.Command.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;

namespace Ambev.Sale.Command.Infrastructure.Orm.Repository;

public class SaleItemCommandRepository : CommandRepositoryBase<Ambev.Sale.Command.Domain.Entities.SaleItem, Guid>, ISaleItemCommandRepository
{
    private readonly SaleCommandDbContext _SaleCommandDbContext;

    public SaleItemCommandRepository(SaleCommandDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleCommandDbContext = defaultDbContext;
    }   
}

