using Hx.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Hx.DictManagement.Application.Contracts
{
    public class DictManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            // 创建权限组
            var dictGroup = context.AddGroup(
                name: DictManagementPermissions.GroupName,
                displayName: L("Permission:DictManagement"));

            // 字典主权限
            var dictPermission = dictGroup.AddPermission(
                name: DictManagementPermissions.Dict.Default,
                displayName: L("Permission:DictManagement.Dict"));

            // 添加子权限
            dictPermission.AddChild(
                name: DictManagementPermissions.Dict.Create,
                displayName: L("Permission:DictManagement.Dict.Create"));

            dictPermission.AddChild(
                name: DictManagementPermissions.Dict.Update,
                displayName: L("Permission:DictManagement.Dict.Update"));

            dictPermission.AddChild(
                name: DictManagementPermissions.Dict.Delete,
                displayName: L("Permission:DictManagement.Dict.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DictManagementResource>(name);
        }
    }
}