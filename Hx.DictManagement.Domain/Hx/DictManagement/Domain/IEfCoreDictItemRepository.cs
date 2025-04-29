using System.Linq.Expressions;
using Volo.Abp.Domain.Repositories;

namespace Hx.DictManagement.Domain
{
    public interface IEfCoreDictItemRepository : IBasicRepository<DictItem, Guid>
    {
        // 基础查询
        Task<DictItem?> FindByCodeAsync(Guid dictTypeId, string code);
        Task<DictItem?> GetWithChildrenAsync(Guid id);

        // 存在性校验
        Task<bool> CodeExistsAsync(Guid dictTypeId, string code, Guid? excludeId = null);
        Task<bool> AnyAsync(Expression<Func<DictItem, bool>> predicate);

        // 结构化查询
        Task<List<DictItem>> GetListByTypeCodeAsync(string typeCode);
        Task<List<DictItem>> GetListByDictTypeIdAsync(
            Guid dictTypeId,
            Guid? parentId = null,
            bool? status = null,
            bool includeChildren = false);

        // 统计查询
        Task<int> GetMaxOrderAsync(Guid dictTypeId);
        Task<int> GetChildCountAsync(Guid parentId);
    }
}
