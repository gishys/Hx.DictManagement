using Hx.DictManagement.Application.Contracts;
using Hx.DictManagement.Domain;
using Hx.DictManagement.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Hx.DictManagement.Application
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpDddApplicationModule))]
    [DependsOn(typeof(HxDictManagementDomainModule))]
    [DependsOn(typeof(HxDictManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(HxDictManagementApplicationContractsModule))]
    public class HxDictManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<HxDictManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DictManagementApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
