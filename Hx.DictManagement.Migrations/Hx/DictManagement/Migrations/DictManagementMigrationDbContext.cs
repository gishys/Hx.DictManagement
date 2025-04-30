using Hx.DictManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.DictManagement.Migrations
{
    public class DictManagementMigrationDbContext(DbContextOptions<DictManagementMigrationDbContext> options)
        : AbpDbContext<DictManagementMigrationDbContext>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureDictManagement();
        }
    }
}
