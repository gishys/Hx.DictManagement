namespace Hx.DictManagement.Application.Contracts
{
    public class DictTypeGroupDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 分组标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 路径枚举
        /// </summary>
        public required string Code { get; set; }
        /// <summary>
        /// 分组排序
        /// </summary>
        public double Order { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 分组描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Get multi tenant id
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 子组
        /// </summary>
        public required List<DictTypeGroupDto> Children { get; set; }
        /// <summary>
        /// 字典组
        /// </summary>
        public required List<DictTypeTreeDto> Items { get; set; }
    }
}
