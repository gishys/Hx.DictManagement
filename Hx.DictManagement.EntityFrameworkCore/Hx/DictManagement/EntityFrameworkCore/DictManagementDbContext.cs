using Hx.DictManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.DictManagement.EntityFrameworkCore
{
    [ConnectionStringName(DictManagementDbProperties.ConnectionStringName)]
    public class DictManagementDbContext(
        DbContextOptions<DictManagementDbContext> options)
        : AbpDbContext<DictManagementDbContext>(options)
    {
        public virtual DbSet<DictType> DictTypes { get; set; }
        public virtual DbSet<DictItem> DictItems { get; set; }
        public virtual DbSet<DictTypeGroup> DictTypeGroups { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureDictManagement();
        }
    }
}