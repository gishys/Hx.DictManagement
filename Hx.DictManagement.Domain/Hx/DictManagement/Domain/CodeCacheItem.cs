using System;

namespace Hx.DictManagement.Domain
{
    public class CodeCacheItem(Guid? parentId, string code)
    {
        public Guid? ParentId { get; set; } = parentId;
        public string Code { get; set; } = code;
    }
}
