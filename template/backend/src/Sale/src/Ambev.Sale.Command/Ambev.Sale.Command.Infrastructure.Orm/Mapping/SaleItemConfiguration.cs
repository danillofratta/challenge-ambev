using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Ambev.Sale.Command.Infrastructure.Orm.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<Ambev.Sale.Command.Domain.Entities.SaleItem>
{
    public void Configure(EntityTypeBuilder<Ambev.Sale.Command.Domain.Entities.SaleItem> builder)
    {
        builder.HasKey(u => u.Id);
    }
}

