namespace Hx.DictManagement.Application.Contracts
{
    public static class DictManagementPermissions
    {
        public const string GroupName = "DictManagement";
        public static class Dict
        {
            public const string Default = GroupName + ".Dict";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}
