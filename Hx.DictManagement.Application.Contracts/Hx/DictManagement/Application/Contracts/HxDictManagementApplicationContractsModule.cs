using Hx.DictManagement.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Hx.DictManagement.Application.Contracts
{
    [DependsOn(typeof(AbpDddApplicationContractsModule))]
    [DependsOn(typeof(HxDictManagementDomainSharedModule))]
    public class HxDictManagementApplicationContractsModule : AbpModule
    {
    }
}
