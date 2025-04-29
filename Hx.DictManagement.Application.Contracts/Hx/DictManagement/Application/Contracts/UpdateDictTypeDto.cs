using Hx.DictManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hx.DictManagement.Application.Contracts
{
    public class UpdateDictTypeDto
    {
        [Required]
        [StringLength(DictManagementConsts.NameMaxLength)]
        public required string Name { get; set; }

        [StringLength(DictManagementConsts.DescriptionMaxLength)]
        public string? Description { get; set; }

        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
