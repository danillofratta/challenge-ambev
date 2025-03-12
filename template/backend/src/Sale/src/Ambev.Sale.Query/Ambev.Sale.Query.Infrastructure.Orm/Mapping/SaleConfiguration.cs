using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Ambev.Sale.Query.Infrastructure.Orm.Mapping;
public class SaleConfiguration : IEntityTypeConfiguration<Ambev.Sale.Query.Domain.Entities.Sale>
{
    public void Configure(EntityTypeBuilder<Ambev.Sale.Query.Domain.Entities.Sale> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(s => s.SaleItens)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

