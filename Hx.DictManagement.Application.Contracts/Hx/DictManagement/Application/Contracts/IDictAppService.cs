using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hx.DictManagement.Application.Contracts
{
    public interface IDictAppService : IApplicationService
    {
        Task<DictTypeDto> CreateDictTypeAsync(CreateDictTypeDto input);
        Task<DictTypeDto> UpdateDictTypeAsync(Guid id, UpdateDictTypeDto input);
        Task DeleteDictTypeAsync(Guid id);
        Task<ListResultDto<DictTypeDto>> GetAllDictTypesAsync();

        Task<DictItemDto> CreateDictItemAsync(CreateDictItemDto input);
        Task<DictItemDto> UpdateDictItemAsync(Guid id, UpdateDictItemDto input);
        Task DeleteDictItemAsync(Guid id);
        Task<DictItemDto?> GetDictItemTreeAsync(string typeCode);
    }
}
