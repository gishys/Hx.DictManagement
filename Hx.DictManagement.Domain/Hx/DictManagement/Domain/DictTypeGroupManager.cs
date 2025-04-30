using Volo.Abp.Caching;

namespace Hx.DictManagement.Domain
{
    public class DictTypeGroupManager(
    IDistributedCache<SortCacheItem> sortCache,
    IEfCoreDictTypeGroupRepository groupRepository,
    IDistributedCache<CodeCacheItem> codeCache) : BaseGroupManager<IEfCoreDictTypeGroupRepository>
        (sortCache, groupRepository, codeCache)
    {
        protected override string CachePrefix => "DictTypeGroup";
    }
}
