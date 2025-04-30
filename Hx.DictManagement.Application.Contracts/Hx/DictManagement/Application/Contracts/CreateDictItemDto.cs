using Hx.DictManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hx.DictManagement.Application.Contracts
{
    public class CreateDictItemDto
    {
        [Required]
        public Guid DictTypeId { get; set; }
        [Required]
        [StringLength(DictManagementConsts.NameMaxLength)]
        public required string Name { get; set; }
        [Required]
        [StringLength(DictManagementConsts.CodeMaxLength)]
        public required string Code { get; set; }
        [Required]
        [StringLength(DictManagementConsts.ValueMaxLength)]
        public required string Value { get; set; }
        public bool Status { get; set; } = true;
        public double Order { get; set; }
        public string? CssClass { get; set; }
        public bool? IsDefault { get; set; }
        public Guid? ParentId { get; set; }
    }

}
