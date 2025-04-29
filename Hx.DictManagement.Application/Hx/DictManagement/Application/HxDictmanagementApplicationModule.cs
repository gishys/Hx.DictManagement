using Hx.DictManagement.Application.Contracts;
using Hx.DictManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Hx.DictManagement.Application
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpDddApplicationModule))]
    [DependsOn(typeof(HxDictManagementDomainModule))]
    [DependsOn(typeof(HxDictManagementApplicationContractsModule))]
    public class HxDictmanagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<HxDictmanagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DictManagementApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
