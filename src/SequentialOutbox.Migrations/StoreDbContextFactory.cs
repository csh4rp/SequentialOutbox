using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SequentialOutbox.Infrastructure.Database.Contexts;

namespace SequentialOutbox.Migrations;

public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

        optionsBuilder.UseNpgsql(args[0], opt =>
        {
            opt.MigrationsAssembly(GetType().Assembly.FullName);
        });

        return new StoreDbContext(optionsBuilder.Options);
    }
}