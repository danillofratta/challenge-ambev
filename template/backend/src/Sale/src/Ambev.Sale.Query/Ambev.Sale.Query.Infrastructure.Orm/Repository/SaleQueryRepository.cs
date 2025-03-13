using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Sale.Query.Infrastructure.Orm.Repository;

public class SaleQueryRepository : QueryRepositoryBase<Ambev.Sale.Query.Domain.Entities.Sale , Guid>, ISaleQueryRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleQueryRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;    
    }

    public async Task<Ambev.Sale.Query.Domain.Entities.Sale> GetByIdAsync(Guid id)
    {
        return await _SaleQueryDbContext.Sales
            .Include(entity => entity.SaleItens) 
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id ==  id);
    }
}
