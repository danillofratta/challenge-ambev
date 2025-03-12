using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Ambev.Sale.Query.Infrastructure.Orm.Mapping;
public class SaleConfiguration : IEntityTypeConfiguration<Ambev.Sale.Query.Domain.Entities.Sale>
{
    public void Configure(EntityTypeBuilder<Ambev.Sale.Query.Domain.Entities.Sale> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Number).IsRequired();
        builder.Property(u => u.Status).IsRequired();

        builder.Property(u => u.BranchId).IsRequired().HasMaxLength(100);
        builder.Property(u => u.BranchName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.CustomerId).IsRequired().HasMaxLength(100);
        builder.Property(u => u.CustomerName).IsRequired().HasMaxLength(200);

        builder.Property(u => u.TotalAmount).IsRequired();

        //builder.Property(u => u.Status)
        //    .HasConversion<string>()
        //    .HasMaxLength(20);

        builder.HasMany(s => s.SaleItens)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

