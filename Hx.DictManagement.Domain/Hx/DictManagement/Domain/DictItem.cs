using Hx.DictManagement.Domain.Shared;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Hx.DictManagement.Domain
{
    public class DictItem : Entity<Guid>
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑添加 "required" 修饰符或声明为可为 null。
        public DictItem() { }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑添加 "required" 修饰符或声明为可为 null。
        public DictItem(
            Guid id,
            string name,
            string code,
            string value,
            bool status,
            double order,
            string? cssClass,
            bool? isDefault,
            Guid? parentId) : base(id)
        {
            Name = name;
            Code = code;
            Value = value;
            Status = status;
            Order = order;
            CssClass = cssClass;
            IsDefault = isDefault;
            ParentId = parentId;
            Children = [];
        }
        /// <summary>
        /// 关联
        /// </summary>
        public virtual Guid DictTypeId { get; protected set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string Name { get; protected set; }
        /// <summary>
        /// 值编码（唯一）
        /// </summary>
        public virtual string Code { get; protected set; }
        /// <summary>
        /// 实际存储值
        /// </summary>
        public virtual string Value { get; protected set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual Guid? ParentId { get; protected set; }
        /// <summary>
        /// 状态（1-启用 0-禁用）
        /// </summary>
        public virtual bool Status { get; protected set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual double Order { get; protected set; }
        /// <summary>
        /// 样式类名
        /// </summary>
        public virtual string? CssClass { get; protected set; }
        /// <summary>
        /// 是否默认值
        /// </summary>
        public virtual bool? IsDefault { get; protected set; }
        /// <summary>
        /// 子项
        /// </summary>
        public virtual ICollection<DictItem> Children { get; protected set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new UserFriendlyException(message: "字典项名称不能为空", code: nameof(name));

            if (name.Length > DictManagementConsts.NameMaxLength)
                throw new UserFriendlyException(message: $"名称长度不能超过{DictManagementConsts.NameMaxLength}个字符");

            Name = name.Trim();
        }

        public void SetValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new UserFriendlyException(message: "字典项值不能为空", code: nameof(value));

            if (value.Length > DictManagementConsts.ValueMaxLength)
                throw new UserFriendlyException(message: $"值长度不能超过{DictManagementConsts.ValueMaxLength}个字符");

            Value = value.Trim();
        }

        public void SetStatus(bool status)
        {
            if (!status && IsDefault.HasValue && IsDefault.Value)
                throw new UserFriendlyException(code: "Dict:DefaultItemCannotDisable", message: "默认字典项不可禁用");

            Status = status;
        }

        public void SetOrder(double order)
        {
            if (order < 0)
                throw new UserFriendlyException(message: "排序值不能小于0", code: nameof(order));

            Order = order;
        }

        public void SetCssClass(string? cssClass)
        {
            if (cssClass?.Length > DictManagementConsts.CssClassMaxLength)
                throw new UserFriendlyException(message: $"样式类长度不能超过{DictManagementConsts.CssClassMaxLength}个字符");

            CssClass = cssClass?.Trim();
        }

        public void SetIsDefault(bool? isDefault)
        {
            if (isDefault.HasValue && isDefault.Value && !Status)
                throw new BusinessException(code: "Dict:DisabledItemCannotBeDefault", message: "禁用状态的项不可设为默认");

            IsDefault = isDefault;
        }

        public async Task SetParentId(Guid? parentId, IEfCoreDictItemRepository repository)
        {
            if (parentId.HasValue)
            {
                if (parentId.Value == Id)
                    throw new BusinessException(code: "Dict:InvalidParent", message: "不能设置自己为父项");

                if (!(await repository.AnyAsync(p => p.Id == parentId.Value)))
                    throw new BusinessException(code: "Dict:ParentNotFound", message: "指定的父项不存在");
            }

            ParentId = parentId;
        }
        public void SetDictTypeId(Guid dictTypeId)
        {
            DictTypeId = dictTypeId;
        }
    }
}