using Volo.Abp.Application.Dtos;

namespace Hx.DictManagement.Application.Contracts
{
    [Serializable]
    public class DictItemDto : EntityDto<Guid>
    {
        public Guid DictTypeId { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Value { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
        public string? CssClass { get; set; }
        public bool? IsDefault { get; set; }
        public Guid? ParentId { get; set; }
        public List<DictItemDto> Children { get; set; } = [];
    }
}
