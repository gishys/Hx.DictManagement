using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Hx.DictManagement.Migrations
{
    public class DictManagementMigrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DictManagementMigrationDbContext>();
        }
    }
}
