namespace Hx.DictManagement.Application.Contracts
{
    public class DictTypeGroupCreateDto : DictTypeGroupCreateOrUpdateDtoBase
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
