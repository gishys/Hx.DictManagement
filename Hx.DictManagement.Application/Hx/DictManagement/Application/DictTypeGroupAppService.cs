using Hx.DictManagement.Application.Contracts;
using Hx.DictManagement.Domain;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Hx.DictManagement.Application
{
    public class DictTypeGroupAppService : ApplicationService, IDictTypeGroupAppService
    {
        private IEfCoreDictTypeGroupRepository GroupRepository { get; }
        private DictTypeGroupManager GroupManager { get; }
        public DictTypeGroupAppService(
            IEfCoreDictTypeGroupRepository groupRepository,
            DictTypeGroupManager groupManager)
        {
            GroupRepository = groupRepository;
            GroupManager = groupManager;
        }
        public async virtual Task CreateAsync(DictTypeGroupCreateDto dto)
        {
            if (await GroupRepository.ExistByTitleAsync(dto.Title))
            {
                throw new UserFriendlyException(message: "已存在相同标题的模板组！");
            }
            var orderNumber = await GroupManager.GetNextOrderNumberAsync(dto.ParentId);
            var code = await GroupManager.GetNextCodeAsync(dto.ParentId);
            var entity = new DictTypeGroup(GuidGenerator.Create(), dto.Title, code, orderNumber, dto.ParentId, dto.Description);
            await GroupRepository.InsertAsync(entity);
        }
        public async virtual Task UpdateAsync(DictTypeGroupUpdateDto dto)
        {
            var entity = await GroupRepository.GetAsync(dto.Id);
            if (!string.Equals(entity.Title, dto.Title, StringComparison.OrdinalIgnoreCase))
            {
                entity.SetTitle(dto.Title);
            }
            if (!string.Equals(entity.Description, dto.Description, StringComparison.OrdinalIgnoreCase))
            {
                entity.SetDescription(dto.Description);
            }
            await GroupRepository.UpdateAsync(entity);
        }
        public async virtual Task DeleteAsync(Guid id)
        {
            var group = await GroupRepository.GetByIdAsync(id) ?? throw new UserFriendlyException(message: "指定的字典组不存在");
            if (group.Children.Count != 0)
            {
                throw new UserFriendlyException(message: "存在子项不能删除");
            }
            if (group.Items.Count != 0)
            {
                throw new UserFriendlyException(message: "存在静态字段不能删除");
            }
            await GroupRepository.DeleteAsync(id);
        }
        public async virtual Task<List<DictTypeGroupDto>> GetAllWithChildrenAsync()
        {
            List<DictTypeGroup> list = await GroupRepository.GetAllWithChildrenAsync(true);
            return ObjectMapper.Map<List<DictTypeGroup>, List<DictTypeGroupDto>>(list);
        }
    }
}
