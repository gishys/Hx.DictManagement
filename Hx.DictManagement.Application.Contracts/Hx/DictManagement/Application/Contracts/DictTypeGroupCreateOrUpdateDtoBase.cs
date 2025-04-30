namespace Hx.DictManagement.Application.Contracts
{
    public class DictTypeGroupCreateOrUpdateDtoBase
    {
        /// <summary>
        /// 分组标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 分组描述
        /// </summary>
        public string? Description { get; set; }
    }
}
