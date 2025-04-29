using Hx.DictManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.DictManagement.EntityFrameworkCore
{
    public class DictManagementDbContext(
        DbContextOptions<DictManagementDbContext> options)
        : AbpDbContext<DictManagementDbContext>(options)
    {
        public virtual DbSet<DictType> Archives { get; set; }
        public virtual DbSet<DictItem> Metadata { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
