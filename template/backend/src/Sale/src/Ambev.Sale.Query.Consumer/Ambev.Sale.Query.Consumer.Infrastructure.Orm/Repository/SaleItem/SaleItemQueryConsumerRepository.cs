using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.Sale.Query.Consumer.Infrastructure.Orm.Repository.SaleItem;
public class SaleItemQueryConsumerRepository : QueryRepositoryBase<Query.Domain.Entities.SaleItem, Guid>, ISaleItemQueryConsumerRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleItemQueryConsumerRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;
    }
}



