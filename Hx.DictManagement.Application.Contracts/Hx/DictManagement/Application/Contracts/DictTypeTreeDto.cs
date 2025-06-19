using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hx.DictManagement.Application.Contracts
{
    public class DictTypeTreeDto : AuditedEntityDto<Guid>
    {
        public required string Title { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public double Order { get; set; }
        public bool IsStatic { get; set; }
    }
}
