using Hx.DictManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hx.DictManagement.Application.Contracts
{
    public class UpdateDictItemDto
    {
        [Required]
        [StringLength(DictManagementConsts.NameMaxLength)]
        public required string Name { get; set; }
        [Required]
        [StringLength(DictManagementConsts.ValueMaxLength)]
        public required string Value { get; set; }
        public bool Status { get; set; }
        public double Order { get; set; }
        public string? CssClass { get; set; }
        public bool? IsDefault { get; set; }
        public Guid? ParentId { get; set; }
    }
}
