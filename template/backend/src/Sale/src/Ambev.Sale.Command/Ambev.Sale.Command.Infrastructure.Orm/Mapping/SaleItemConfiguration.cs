using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Ambev.Sale.Command.Infrastructure.Orm.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<Ambev.Sale.Command.Domain.Entities.SaleItem>
{
    public void Configure(EntityTypeBuilder<Ambev.Sale.Command.Domain.Entities.SaleItem> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.Status).IsRequired();
        builder.Property(u => u.TotalPrice).IsRequired();
        builder.Property(u => u.UnitPrice).IsRequired();

        builder.Property(u => u.ProductId).IsRequired().HasMaxLength(100);
        builder.Property(u => u.ProductName).IsRequired().HasMaxLength(200);

        //builder.Property(u => u.Status)
        //    .HasConversion<string>()
        //    .HasMaxLength(20);
    }
}

