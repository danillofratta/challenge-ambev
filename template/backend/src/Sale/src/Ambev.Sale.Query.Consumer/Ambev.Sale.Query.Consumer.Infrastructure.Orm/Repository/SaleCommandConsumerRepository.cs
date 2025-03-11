
using Ambev.Base.Infrastructure.Command.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository;
using System;


namespace Ambev.Sale.Query.Consumer.Infrastructure.Orm;

public class SaleCommandConsumerRepository : CommandRepositoryBase<Ambev.Sale.Query.Domain.Entities.Sale, Guid>, ISaleCommandConsumerRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleCommandConsumerRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;
    }

}


