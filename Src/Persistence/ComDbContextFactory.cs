using Microsoft.EntityFrameworkCore;

namespace Com.Persistence;
public class ComDbContextFactory : DesignTimeDbContextFactoryBase<ComDbContext>
{
    protected override ComDbContext CreateNewInstance(DbContextOptions<ComDbContext> options)
    {
        return new ComDbContext(options);
    }
}
