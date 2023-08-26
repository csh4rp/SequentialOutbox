using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EStore.Infrastructure.Database.Contexts;

namespace EStore.Migrations;

public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

        optionsBuilder.UseSqlServer(args[0], opt =>
        {
            opt.MigrationsAssembly(GetType().Assembly.FullName);
        });

        return new StoreDbContext(optionsBuilder.Options);
    }
}