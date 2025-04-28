using System.Linq.Expressions;
using Volo.Abp.Domain.Repositories;

namespace Hx.DictManagement.Domain
{
    public interface IEfCoreDictTypeRepository : IBasicRepository<DictType, Guid>
    {
        // 基础查询
        Task<DictType?> FindByCodeAsync(string code);
        Task<DictType?> FindByNameAsync(string name);

        // 存在性校验
        Task<bool> CodeExistsAsync(string code, Guid? excludeId = null);
        Task<bool> AnyAsync(Expression<Func<DictType, bool>> predicate);

        // 高级查询
        Task<List<DictType>> GetListAsync(
            string? filter = null,
            bool? status = null,
            bool includeDetails = false);
    }
}
