using Hx.DictManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hx.DictManagement.EntityFrameworkCore
{
    public class EfCoreDictTypeRepository(IDbContextProvider<DictManagementDbContext> dbContextProvider)
        : EfCoreRepository<DictManagementDbContext, DictType, Guid>(dbContextProvider), IEfCoreDictTypeRepository
    {
        public async Task<DictType?> FindByCodeAsync(string code)
        {
            return await (await GetQueryableAsync())
                .AsNoTracking()
                .FirstOrDefaultAsync(dt => dt.Code == code);
        }

        public async Task<DictType?> FindByNameAsync(string name)
        {
            return await (await GetQueryableAsync())
                .AsNoTracking()
                .FirstOrDefaultAsync(dt => dt.Name == name);
        }

        public async Task<bool> CodeExistsAsync(string code, Guid? excludeId = null)
        {
            var query = (await GetQueryableAsync())
                .Where(dt => dt.Code == code);

            if (excludeId.HasValue)
            {
                query = query.Where(dt => dt.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<List<DictType>> GetListAsync(
            string? filter = null,
            bool? status = null,
            bool includeDetails = false)
        {
            var query = await GetQueryableAsync();

            if (includeDetails)
            {
                query = query.Include(dt => dt.DictItems);
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = $"%{filter}%";
                query = query.Where(dt =>
                    EF.Functions.Like(dt.Name, filter) ||
                    EF.Functions.Like(dt.Code, filter));
            }

            if (status.HasValue)
            {
                query = query.Where(dt => dt.Status == status.Value);
            }

            return await query
                .OrderBy(dt => dt.Order)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<DictType, bool>> predicate)
        {
            return await (await GetQueryableAsync()).AnyAsync(predicate);
        }
    }
}
