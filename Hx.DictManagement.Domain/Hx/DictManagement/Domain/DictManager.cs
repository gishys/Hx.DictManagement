using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Hx.DictManagement.Domain
{
    public class DictManager(
        IGuidGenerator guidGenerator,
        IEfCoreDictTypeRepository dictTypeRepository,
        IEfCoreDictItemRepository dictItemRepository) : DomainService
    {
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IEfCoreDictTypeRepository _dictTypeRepository = dictTypeRepository;
        private readonly IEfCoreDictItemRepository _dictItemRepository = dictItemRepository;

        #region DictType 操作
        public async Task<DictType> CreateDictTypeAsync(
            string name,
            string code,
            string? description,
            bool status,
            int order,
            bool isStatic)
        {
            // 校验编码唯一性
            if (await _dictTypeRepository.AnyAsync(dt => dt.Code == code))
            {
                throw new UserFriendlyException(message: "字典类型编码已存在");
            }

            return new DictType(
                _guidGenerator.Create(),
                name,
                code,
                description,
                status,
                order,
                isStatic);
        }

        public async Task<DictType> UpdateDictTypeAsync(
            Guid id,
            string name,
            string description,
            bool status,
            int order)
        {
            DictType dictType = await _dictTypeRepository.GetAsync(id);

            dictType.SetName(name);
            dictType.SetDescription(description);
            dictType.SetStatus(status);
            dictType.SetOrder(order);

            return dictType;
        }

        public async Task DeleteDictTypeAsync(Guid id)
        {
            var dictType = await _dictTypeRepository.GetAsync(id);

            if (dictType.IsStatic)
            {
                throw new UserFriendlyException(code: "Dict:StaticDictTypeCannotDelete", message: "系统内置字典类型不可删除");
            }

            await _dictTypeRepository.DeleteAsync(dictType);
        }
        #endregion

        #region DictItem 操作
        public async Task<DictItem> AddDictItemAsync(
            Guid dictTypeId,
            string name,
            string code,
            string value,
            bool status,
            int order,
            string? cssClass = null,
            bool? isDefault = null,
            Guid? parentId = null)
        {
            // 校验字典类型存在
            var dictType = await _dictTypeRepository.GetAsync(dictTypeId);

            // 校验同一类型下编码唯一性
            if (await _dictItemRepository.AnyAsync(di => di.DictTypeId == dictTypeId && di.Code == code))
            {
                throw new UserFriendlyException("字典项编码已存在");
            }

            // 校验父项是否存在
            if (parentId.HasValue && !await _dictItemRepository.AnyAsync(di => di.ParentId == parentId.Value))
            {
                throw new UserFriendlyException("指定的父项不存在");
            }

            var dictItem = new DictItem(
                _guidGenerator.Create(),
                name,
                code,
                value,
                status,
                order,
                cssClass,
                isDefault,
                parentId);
            dictItem.SetDictTypeId(dictTypeId);
            return dictItem;
        }

        public async Task<DictItem> UpdateDictItemAsync(
            Guid itemId,
            string name,
            string value,
            bool status,
            int order,
            string? cssClass = null,
            bool? isDefault = null,
            Guid? parentId = null)
        {
            var dictItem = await _dictItemRepository.GetAsync(itemId);

            dictItem.SetName(name);
            dictItem.SetValue(value);
            dictItem.SetStatus(status);
            dictItem.SetOrder(order);
            dictItem.SetCssClass(cssClass);
            dictItem.SetIsDefault(isDefault);

            // 更新父项时需要校验
            if (parentId.HasValue && dictItem.ParentId != parentId)
            {
                if (!await _dictItemRepository.AnyAsync(di => di.ParentId == parentId.Value))
                {
                    throw new UserFriendlyException("指定的父项不存在");
                }
                await dictItem.SetParentId(parentId.Value, _dictItemRepository);
            }

            return dictItem;
        }

        public async Task DeleteDictItemAsync(Guid itemId)
        {
            var item = await _dictItemRepository.GetAsync(itemId);

            if (item.Children.Count > 0)
            {
                throw new UserFriendlyException(code: "Dict:CannotDeleteItemWithChildren", message: "存在子项时不可删除");
            }

            await _dictItemRepository.DeleteAsync(item);
        }
        #endregion

        #region 查询方法
        public async Task<DictType?> FindByTypeCodeAsync(string code)
        {
            return await _dictTypeRepository.FindByCodeAsync(code);
        }

        public async Task<List<DictItem>> GetItemsByTypeCodeAsync(string code)
        {
            var dictType = await _dictTypeRepository.FindByCodeAsync(code);
            return dictType?.DictItems.ToList() ?? new List<DictItem>();
        }

        public async Task<List<DictItem>> GetTreeByTypeCodeAsync(string code)
        {
            var items = await _dictItemRepository.GetListByTypeCodeAsync(code);
            return BuildTree(items);
        }

        private static List<DictItem> BuildTree(List<DictItem> items)
        {
            var tree = new List<DictItem>();
            var itemDict = items.ToDictionary(i => i.Id);

            foreach (var item in items)
            {
                if (item.ParentId == null)
                {
                    tree.Add(item);
                }
                else if (itemDict.TryGetValue(item.ParentId.Value, out var parent))
                {
                    parent.Children.Add(item);
                }
            }
            return tree;
        }
        #endregion
    }
}
