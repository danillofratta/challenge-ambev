using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository;
using System;

namespace Ambev.Sale.Query.Consumer.Infrastructure.Orm;
public class SaleQueryConsumerRepository : QueryRepositoryBase<Ambev.Sale.Query.Domain.Entities.Sale, Guid>, ISaleQueryConsumerRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleQueryConsumerRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;
    }
}



