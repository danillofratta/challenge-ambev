using Ambev.Base.Infrastructure.Command.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using System;


namespace Ambev.Sale.Query.Consumer.Infrastructure.Orm.Repository.Sale;

public class SaleCommandConsumerRepository : CommandRepositoryBase<Query.Domain.Entities.Sale, Guid>, ISaleCommandConsumerRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleCommandConsumerRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;
    }

}


