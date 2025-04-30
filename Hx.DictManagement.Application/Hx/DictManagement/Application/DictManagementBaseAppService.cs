using Hx.Localization;
using Volo.Abp.Application.Services;

namespace Hx.DictManagement.Application
{
    public class DictManagementBaseAppService : ApplicationService
    {
        public DictManagementBaseAppService()
        {
            LocalizationResource = typeof(DictManagementResource);
        }
        public string GetLocalization(string name)
        {
            return L[name];
        }
    }
}
