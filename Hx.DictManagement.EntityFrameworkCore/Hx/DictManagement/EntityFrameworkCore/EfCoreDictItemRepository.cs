using Hx.DictManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.DictManagement.EntityFrameworkCore
{
    public class EfCoreDictItemRepository(IDbContextProvider<DictManagementDbContext> dbContextProvider)
        : EfCoreRepository<DictManagementDbContext, DictItem, Guid>(dbContextProvider), IEfCoreDictItemRepository
    {
        public async Task<DictItem?> FindByCodeAsync(Guid dictTypeId, string code)
        {
            return await (await GetQueryableAsync())
                .AsNoTracking()
                .FirstOrDefaultAsync(di =>
                    di.DictTypeId == dictTypeId &&
                    di.Code == code);
        }

        public async Task<DictItem?> GetWithChildrenAsync(Guid id)
        {
            var item = await (await GetQueryableAsync())
                .Include(di => di.Children)
                .FirstOrDefaultAsync(di => di.Id == id);

            if (item != null)
            {
                // 递归加载子项
                foreach (var child in item.Children.ToList())
                {
                    await LoadChildrenAsync(child);
                }
            }

            return item;
        }

        private async Task LoadChildrenAsync(DictItem item)
        {
            await (await GetDbContextAsync()).Entry(item)
                .Collection(di => di.Children)
                .LoadAsync();

            foreach (var child in item.Children.ToList())
            {
                await LoadChildrenAsync(child);
            }
        }

        public async Task<bool> CodeExistsAsync(Guid dictTypeId, string code, Guid? excludeId = null)
        {
            var query = (await GetQueryableAsync())
                .Where(di =>
                    di.DictTypeId == dictTypeId &&
                    di.Code == code);

            if (excludeId.HasValue)
            {
                query = query.Where(di => di.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<List<DictItem>> GetListByTypeCodeAsync(string typeCode)
        {
            var query = from item in await GetQueryableAsync()
                        join type in (await GetDbContextAsync()).Set<DictType>()
                            on item.DictTypeId equals type.Id
                        where type.Code == typeCode
                        select item;

            return await query
                .OrderBy(di => di.Order)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<DictItem>> GetListByDictTypeIdAsync(
            Guid dictTypeId,
            Guid? parentId = null,
            bool? status = null,
            bool includeChildren = false)
        {
            var query = (await GetQueryableAsync())
                .Where(di => di.DictTypeId == dictTypeId);

            if (parentId.HasValue)
            {
                query = query.Where(di => di.ParentId == parentId);
            }
            else
            {
                query = query.Where(di => di.ParentId == null);
            }

            if (status.HasValue)
            {
                query = query.Where(di => di.Status == status.Value);
            }

            if (includeChildren)
            {
                query = query.Include(di => di.Children);
            }

            return await query
                .OrderBy(di => di.Order)
                .AsNoTracking()
                .ToListAsync();
        }

        public async override Task<DictItem?> FindAsync(Expression<Func<DictItem, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            if(includeDetails)
            {
                query = query.Include(d => d.Children);
            }
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<int> GetMaxOrderAsync(Guid dictTypeId)
        {
            return await (await GetQueryableAsync())
                .Where(di => di.DictTypeId == dictTypeId)
                .MaxAsync(di => (int?)di.Order) ?? 0;
        }

        public async Task<int> GetChildCountAsync(Guid parentId)
        {
            return await (await GetQueryableAsync())
                .CountAsync(di => di.ParentId == parentId);
        }

        public async Task<bool> AnyAsync(Expression<Func<DictItem, bool>> predicate)
        {
            return await (await GetQueryableAsync()).AnyAsync(predicate);
        }
    }
}
