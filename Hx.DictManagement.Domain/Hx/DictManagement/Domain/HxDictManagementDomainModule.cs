using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Hx.DictManagement.Domain
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class HxDictManagementDomainModule : AbpModule
    {
    }
}
