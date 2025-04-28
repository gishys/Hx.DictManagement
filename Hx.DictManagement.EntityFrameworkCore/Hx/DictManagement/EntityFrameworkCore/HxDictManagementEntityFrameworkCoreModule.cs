using Hx.DictManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Hx.DictManagement.EntityFrameworkCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
    public class HxDictManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            context.Services.AddAbpDbContext<DictManagementDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
            context.Services.AddAbpDbContext<DictManagementDbContext>(options =>
            {
                options.AddRepository<DictType, EfCoreDictTypeRepository>();
                options.AddRepository<DictItem, EfCoreDictItemRepository>();
            });
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseNpgsql(options =>
                {
                });
            });
        }
    }
}
