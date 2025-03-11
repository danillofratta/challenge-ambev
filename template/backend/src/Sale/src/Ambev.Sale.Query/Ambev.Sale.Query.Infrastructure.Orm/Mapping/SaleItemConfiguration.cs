using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Ambev.Sale.Query.Infrastructure.Orm.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<Ambev.Sale.Query.Domain.Entities.SaleItem>
{
    public void Configure(EntityTypeBuilder<Ambev.Sale.Query.Domain.Entities.SaleItem> builder)
    {
        builder.HasKey(u => u.Id);        
    }
}

