using Hx.DictManagement.Application.Contracts;
using Hx.DictManagement.Domain;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hx.DictManagement.Application
{
    //[Authorize("")]
    public class DictAppService(
        DictManager dictManager,
        IEfCoreDictTypeRepository typeRepository,
        IEfCoreDictItemRepository itemRepository) : ApplicationService, IDictAppService
    {
        private readonly DictManager _dictManager = dictManager;
        private readonly IEfCoreDictTypeRepository _typeRepository = typeRepository;
        private readonly IEfCoreDictItemRepository _itemRepository = itemRepository;

        #region DictType 接口
        //[Authorize(DictManagementPermissions.Dict.Create)]
        public async Task<DictTypeDto> CreateDictTypeAsync(CreateDictTypeDto input)
        {
            var dictType = await _dictManager.CreateDictTypeAsync(
                input.Name,
                input.Code,
                input.Description,
                input.Status,
                input.Order,
                input.IsStatic,
                input.GroupId
            );

            await _typeRepository.InsertAsync(dictType);
            return ObjectMapper.Map<DictType, DictTypeDto>(dictType);
        }

        //[Authorize(DictManagementPermissions.Dict.Update)]
        public async Task<DictTypeDto> UpdateDictTypeAsync(Guid id, UpdateDictTypeDto input)
        {
            var dictType = await _typeRepository.GetAsync(id);

            dictType.SetName(input.Name);
            dictType.SetDescription(input.Description);
            dictType.SetStatus(input.Status);
            dictType.SetOrder(input.Order);

            await _typeRepository.UpdateAsync(dictType);
            return ObjectMapper.Map<DictType, DictTypeDto>(dictType);
        }

        //[Authorize(DictManagementPermissions.Dict.Delete)]
        public async Task DeleteDictTypeAsync(Guid id)
        {
            await _dictManager.DeleteDictTypeAsync(id);
        }

        public async Task<ListResultDto<DictTypeDto>> GetAllDictTypesAsync()
        {
            var types = await _typeRepository.GetListAsync();
            return new ListResultDto<DictTypeDto>(
                ObjectMapper.Map<List<DictType>, List<DictTypeDto>>(types)
            );
        }
        #endregion

        #region DictItem 接口
        //[Authorize(DictManagementPermissions.Dict.Create)]
        public async Task<DictItemDto> CreateDictItemAsync(CreateDictItemDto input)
        {
            var dictItem = await _dictManager.AddDictItemAsync(
                input.DictTypeId,
                input.Name,
                input.Code,
                input.Value,
                input.Status,
                input.Order,
                input.CssClass,
                input.IsDefault,
                input.ParentId
            );

            await _itemRepository.InsertAsync(dictItem);
            return ObjectMapper.Map<DictItem, DictItemDto>(dictItem);
        }

        //[Authorize(DictManagementPermissions.Dict.Update)]
        public async Task<DictItemDto> UpdateDictItemAsync(Guid id, UpdateDictItemDto input)
        {
            var dictItem = await _itemRepository.GetAsync(id);

            dictItem.SetName(input.Name);
            if (dictItem.IsDefault == null || (dictItem.IsDefault.HasValue && !dictItem.IsDefault.Value))
                dictItem.SetCode(input.Code);
            dictItem.SetValue(input.Value);
            dictItem.SetStatus(input.Status);
            dictItem.SetOrder(input.Order);
            dictItem.SetCssClass(input.CssClass);
            dictItem.SetIsDefault(input.IsDefault);

            if (dictItem.ParentId != input.ParentId)
            {
                await dictItem.SetParentId(input.ParentId, _itemRepository);
            }

            await _itemRepository.UpdateAsync(dictItem);
            return ObjectMapper.Map<DictItem, DictItemDto>(dictItem);
        }

        //[Authorize(DictManagementPermissions.Dict.Delete)]
        public async Task DeleteDictItemAsync(Guid id)
        {
            await _dictManager.DeleteDictItemAsync(id);
        }

        public async Task<List<DictItemDto>> GetDictItemTreeAsync(string typeCode)
        {
            var items = await _dictManager.GetTreeByTypeCodeAsync(typeCode);
            return BuildTreeDto(items);
        }

        private List<DictItemDto> BuildTreeDto(List<DictItem> items)
        {
            var root = items.Where(i => i.ParentId == null).ToList();
            if (root == null) return [];

            var dtos = ObjectMapper.Map<List<DictItem>, List<DictItemDto>>(root);
            foreach (var dto in dtos)
            {
                BuildTreeChildren(dto, items);
            }
            return dtos;
        }

        private void BuildTreeChildren(DictItemDto parentDto, List<DictItem> allItems)
        {
            var children = allItems.Where(i => i.ParentId == parentDto.Id).ToList();
            foreach (var child in children)
            {
                var childDto = ObjectMapper.Map<DictItem, DictItemDto>(child);
                parentDto.Children.Add(childDto);
                BuildTreeChildren(childDto, allItems);
            }
        }
        #endregion
    }
}
