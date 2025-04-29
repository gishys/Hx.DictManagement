using Hx.DictManagement.Domain;
using Hx.DictManagement.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Hx.DictManagement.EntityFrameworkCore
{
    public static class DictManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureDictManagement(this ModelBuilder builder)
        {
            builder.Entity<DictType>(b =>
            {
                // 表名及基本配置
                b.ToTable("DICT_TYPES");
                b.ConfigureByConvention();

                // 主键
                b.HasKey(dt => dt.Id);
                b.Property(dt => dt.Id).HasColumnName("ID");

                // 索引配置
                b.HasIndex(dt => dt.Code).IsUnique().HasDatabaseName("IX_DICT_TYPES_CODE");
                b.HasIndex(dt => dt.Name).HasDatabaseName("IX_DICT_TYPES_NAME");
                b.HasIndex(dt => dt.Status).HasDatabaseName("IX_DICT_TYPES_STATUS");
                b.HasIndex(dt => dt.Order).HasDatabaseName("IX_DICT_TYPES_ORDER");

                // 属性配置
                b.Property(dt => dt.Name)
                    .HasMaxLength(DictManagementConsts.NameMaxLength)
                    .HasColumnName("NAME")
                    .IsRequired();

                b.Property(dt => dt.Code)
                    .HasMaxLength(DictManagementConsts.CodeMaxLength)
                    .HasColumnName("CODE")
                    .IsRequired();

                b.Property(dt => dt.Description)
                    .HasMaxLength(DictManagementConsts.DescriptionMaxLength)
                    .HasColumnName("DESCRIPTION");

                b.Property(dt => dt.Status)
                    .HasColumnName("STATUS")
                    .IsRequired();

                b.Property(dt => dt.Order)
                    .HasColumnName("ORDER")
                    .IsRequired();

                b.Property(dt => dt.IsStatic)
                    .HasColumnName("IS_STATIC")
                    .IsRequired();

                // 导航属性配置
                b.HasMany(dt => dt.DictItems)
                    .WithOne()
                    .HasForeignKey(di => di.DictTypeId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ABP 基类审计字段配置
                b.Property(p => p.CreationTime).HasColumnName("CREATIONTIME").HasColumnType("timestamp with time zone");
                b.Property(p => p.CreatorId).HasColumnName("CREATORID");
                b.Property(p => p.LastModificationTime).HasColumnName("LASTMODIFICATIONTIME").HasColumnType("timestamp with time zone");
                b.Property(p => p.LastModifierId).HasColumnName("LASTMODIFIERID");
            });

            builder.Entity<DictItem>(b =>
            {
                // 表名及基本配置
                b.ToTable("DICT_ITEMS");
                b.ConfigureByConvention();

                // 主键
                b.HasKey(di => di.Id);
                b.Property(di => di.Id).HasColumnName("ID");

                // 索引配置
                b.HasIndex(di => new { di.DictTypeId, di.Code }).IsUnique().HasDatabaseName("IX_DICT_ITEMS_CODE");
                b.HasIndex(di => di.DictTypeId).HasDatabaseName("IX_DICT_ITEMS_DICT_TYPE_ID");
                b.HasIndex(di => di.ParentId).HasDatabaseName("IX_DICT_ITEMS_PARENT_ID");
                b.HasIndex(di => di.Status).HasDatabaseName("IX_DICT_ITEMS_STATUS");
                b.HasIndex(di => di.Order).HasDatabaseName("IX_DICT_ITEMS_ORDER");
                b.HasIndex(di => new { di.DictTypeId, di.Code }).HasDatabaseName("IX_DICT_ITEMS_DICTTYPEID_CODE");
                b.HasIndex(di => new { di.ParentId, di.Order }).HasDatabaseName("IX_DICT_ITEMS_PARENTID_ORDER");

                // 属性配置
                b.Property(di => di.DictTypeId)
                    .HasColumnName("DICT_TYPE_ID")
                    .IsRequired();

                b.Property(di => di.Name)
                    .HasMaxLength(DictManagementConsts.NameMaxLength)
                    .HasColumnName("NAME")
                    .IsRequired();

                b.Property(di => di.Code)
                    .HasMaxLength(DictManagementConsts.CodeMaxLength)
                    .HasColumnName("CODE")
                    .IsRequired();

                b.Property(di => di.Value)
                    .HasMaxLength(DictManagementConsts.ValueMaxLength)
                    .HasColumnName("VALUE")
                    .IsRequired();

                b.Property(di => di.ParentId)
                    .HasColumnName("PARENT_ID");

                b.Property(di => di.Status)
                    .HasColumnName("STATUS")
                    .IsRequired();

                b.Property(di => di.Order)
                    .HasColumnName("ORDER")
                    .IsRequired();

                b.Property(di => di.CssClass)
                    .HasMaxLength(DictManagementConsts.CssClassMaxLength)
                    .HasColumnName("CSS_CLASS");

                b.Property(di => di.IsDefault)
                    .HasColumnName("IS_DEFAULT");

                // 自引用关系配置
                b.HasMany(di => di.Children)
                    .WithOne()
                    .HasForeignKey(di => di.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
