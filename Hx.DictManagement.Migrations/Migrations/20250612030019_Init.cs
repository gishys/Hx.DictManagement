using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DICT_TYPE_GROUPS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "主键"),
                    TITLE = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "标题"),
                    CODE = table.Column<string>(type: "character varying(119)", maxLength: 119, nullable: false, comment: "路径枚举"),
                    ORDER = table.Column<double>(type: "double precision", nullable: false, comment: "序号"),
                    PARENT_ID = table.Column<Guid>(type: "uuid", nullable: true, comment: "父Id"),
                    DESCRIPTION = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "描述"),
                    TENANTID = table.Column<Guid>(type: "uuid", nullable: true, comment: "租户Id"),
                    CREATIONTIME = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATORID = table.Column<Guid>(type: "uuid", nullable: true),
                    LASTMODIFICATIONTIME = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LASTMODIFIERID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATIONFORM_GROUP", x => x.ID);
                    table.ForeignKey(
                        name: "AF_GROUPS_PARENT_ID",
                        column: x => x.PARENT_ID,
                        principalTable: "DICT_TYPE_GROUPS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DICT_TYPES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    NAME = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CODE = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    STATUS = table.Column<bool>(type: "boolean", nullable: false),
                    ORDER = table.Column<double>(type: "double precision", nullable: false),
                    IS_STATIC = table.Column<bool>(type: "boolean", nullable: false),
                    CREATIONTIME = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATORID = table.Column<Guid>(type: "uuid", nullable: true),
                    LASTMODIFICATIONTIME = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LASTMODIFIERID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DICT_TYPES", x => x.ID);
                    table.ForeignKey(
                        name: "AF_GROUPS_APPLICATIONFORM_ID",
                        column: x => x.GroupId,
                        principalTable: "DICT_TYPE_GROUPS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DICT_ITEMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DICT_TYPE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CODE = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    VALUE = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PARENT_ID = table.Column<Guid>(type: "uuid", nullable: true),
                    STATUS = table.Column<bool>(type: "boolean", nullable: false),
                    ORDER = table.Column<double>(type: "double precision", nullable: false),
                    CSS_CLASS = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IS_DEFAULT = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DICT_ITEMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DICT_ITEMS_DICT_ITEMS_PARENT_ID",
                        column: x => x.PARENT_ID,
                        principalTable: "DICT_ITEMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DICT_ITEMS_DICT_TYPES_DICT_TYPE_ID",
                        column: x => x.DICT_TYPE_ID,
                        principalTable: "DICT_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DICT_ITEMS_DICT_TYPE_ID",
                table: "DICT_ITEMS",
                column: "DICT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_ITEMS_DICTTYPEID_CODE",
                table: "DICT_ITEMS",
                columns: new[] { "DICT_TYPE_ID", "CODE" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DICT_ITEMS_ORDER",
                table: "DICT_ITEMS",
                column: "ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_ITEMS_PARENT_ID",
                table: "DICT_ITEMS",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_ITEMS_PARENTID_ORDER",
                table: "DICT_ITEMS",
                columns: new[] { "PARENT_ID", "ORDER" });

            migrationBuilder.CreateIndex(
                name: "IX_DICT_ITEMS_STATUS",
                table: "DICT_ITEMS",
                column: "STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_TYPE_GROUPS_PARENT_ID",
                table: "DICT_TYPE_GROUPS",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_TYPES_CODE",
                table: "DICT_TYPES",
                column: "CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DICT_TYPES_GroupId",
                table: "DICT_TYPES",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_TYPES_NAME",
                table: "DICT_TYPES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_TYPES_ORDER",
                table: "DICT_TYPES",
                column: "ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_DICT_TYPES_STATUS",
                table: "DICT_TYPES",
                column: "STATUS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DICT_ITEMS");

            migrationBuilder.DropTable(
                name: "DICT_TYPES");

            migrationBuilder.DropTable(
                name: "DICT_TYPE_GROUPS");
        }
    }
}
