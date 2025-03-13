using Ambev.Base.Infrastructure.Query.Orm.Repository;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.Sale.Query.Consumer.Infrastructure.Orm.Repository.Sale;
public class SaleQueryConsumerRepository : QueryRepositoryBase<Query.Domain.Entities.Sale, Guid>, ISaleQueryConsumerRepository
{
    private readonly SaleQueryDbContext _SaleQueryDbContext;

    public SaleQueryConsumerRepository(SaleQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _SaleQueryDbContext = defaultDbContext;
    }
}



