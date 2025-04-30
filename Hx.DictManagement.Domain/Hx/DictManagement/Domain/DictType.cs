using Hx.DictManagement.Domain.Shared;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hx.DictManagement.Domain
{
    public class DictType : AuditedEntity<Guid>
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑添加 "required" 修饰符或声明为可为 null。
        public DictType() { }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑添加 "required" 修饰符或声明为可为 null。
        public DictType(
            Guid id,
            string name,
            string code,
            string? description,
            bool status,
            double order,
            bool isStatic) : base(id)
        {
            Name = name;
            Code = code;
            Description = description;
            Status = status;
            Order = order;
            IsStatic = isStatic;
            DictItems = [];
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public virtual string Name { get; protected set; }
        /// <summary>
        /// 类型编码（唯一）
        /// </summary>
        public virtual string Code { get; protected set; }
        /// <summary>
        /// 类型描述
        /// </summary>
        public virtual string? Description { get; protected set; }
        /// <summary>
        /// 状态（1-启用 0-禁用）
        /// </summary>
        public virtual bool Status { get; protected set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual double Order { get; protected set; }
        /// <summary>
        /// 是否静态
        /// </summary>
        public virtual bool IsStatic { get; protected set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual ICollection<DictItem> DictItems { get; protected set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new UserFriendlyException(message: "字典类型名称不能为空", code: nameof(name));

            if (name.Length > DictManagementConsts.NameMaxLength)
                throw new UserFriendlyException(message: $"名称长度不能超过{DictManagementConsts.NameMaxLength}个字符");

            Name = name.Trim();
        }

        public void SetDescription(string? description)
        {
            if (description?.Length > DictManagementConsts.DescriptionMaxLength)
                throw new UserFriendlyException(message: $"描述长度不能超过{DictManagementConsts.DescriptionMaxLength}个字符");

            Description = description?.Trim();
        }

        public void SetStatus(bool status)
        {
            if (IsStatic && !status)
                throw new UserFriendlyException(code: "Dict:StaticTypeCannotDisable", message: "系统内置字典类型不可禁用");

            Status = status;
        }

        public void SetOrder(double order)
        {
            if (order < 0)
                throw new UserFriendlyException(message: "排序值不能小于0", code: nameof(order));

            Order = order;
        }
    }
}