using Ambev.Sale.Command.Infrastructure.Orm.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.Sale.Command.Infrastructure.Orm;

public class SaleCommandDbContext : DbContext
{
    public DbSet<Ambev.Sale.Command.Domain.Entities.Sale> Sales { get; set; }

    public DbSet<Ambev.Sale.Command.Domain.Entities.SaleItem> SaleItens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

#if DEBUG
            //run docker with VS
            //todo craete appsettings            
            var conn = "Host=localhost;Port=5432;Username=admin;Password=root;Database=SaleWriteDb;";
            optionsBuilder.UseNpgsql(conn);
#else
        //run docker 
        //todo craete appsettings
        var conn = "Host=postgres-container;Port=5432;Username=admin;Password=root;Database=SaleWriteDb;";
        optionsBuilder.UseNpgsql(conn);
#endif

        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new SaleItemConfiguration());
    }
}
