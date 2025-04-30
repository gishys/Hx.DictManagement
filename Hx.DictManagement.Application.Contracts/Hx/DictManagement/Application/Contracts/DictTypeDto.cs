using Volo.Abp.Application.Dtos;

namespace Hx.DictManagement.Application.Contracts
{
    [Serializable]
    public class DictTypeDto : AuditedEntityDto<Guid>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public double Order { get; set; }
        public bool IsStatic { get; set; }
    }
}
