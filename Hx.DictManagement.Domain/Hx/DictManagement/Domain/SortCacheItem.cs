namespace Hx.DictManagement.Domain
{
    public class SortCacheItem(Guid? parentId, double sort)
    {
        public Guid? ParentId { get; set; } = parentId;
        public double Sort { get; set; } = sort;
    }
}
