
using Ambev.Base.Infrastructure.Command.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;
using System;

namespace Ambev.Sale.Query.Consumer.Infrastructure.Orm.Repository.SaleItem;

public class SaleItemCommandConsumerRepository : CommandRepositoryBase<Ambev.Sale.Query.Domain.Entities.SaleItem, Guid>, ISaleItemCommandConsumerRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleItemCommandConsumerRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;
    }

}


