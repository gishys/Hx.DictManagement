namespace Hx.DictManagement.Application.Contracts
{
    public interface IDictTypeGroupAppService
    {
        Task CreateAsync(DictTypeGroupCreateDto dto);
        Task UpdateAsync(DictTypeGroupUpdateDto dto);
        Task DeleteAsync(Guid id);
        Task<List<DictTypeGroupDto>> GetAllWithChildrenAsync();
    }
}
