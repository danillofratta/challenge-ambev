using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Ambev.Sale.Command.Infrastructure.Orm.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Ambev.Sale.Command.Domain.Entities.Sale>
{
    public void Configure(EntityTypeBuilder<Ambev.Sale.Command.Domain.Entities.Sale> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(s => s.SaleItens)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

