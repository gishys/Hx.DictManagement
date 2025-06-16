using Hx.DictManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hx.DictManagement.Application.Contracts
{
    public class CreateDictTypeDto
    {
        [Required]
        [StringLength(DictManagementConsts.NameMaxLength)]
        public required string Name { get; set; }

        [Required]
        [StringLength(DictManagementConsts.CodeMaxLength)]
        public required string Code { get; set; }

        [StringLength(DictManagementConsts.DescriptionMaxLength)]
        public string? Description { get; set; }

        public bool Status { get; set; } = true;
        public double Order { get; set; }
        public bool IsStatic { get; set; }
        public Guid? GroupId { get; set; }
    }
}
