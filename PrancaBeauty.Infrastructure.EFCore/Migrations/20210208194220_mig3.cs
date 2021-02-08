using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e11cd3aa-c497-4f9d-8ed3-acc90008e9e7"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tblAccessLevel_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    AccessLevelId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccessLevel_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAccessLevel_Roles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAccessLevel_Roles_tblAccessLevels_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "tblAccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("96426a38-a89f-4091-a313-acc9017e6a71"), "180eb0ed-35c4-4be4-8023-a0cf4f0081fb", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_tblAccessLevel_Roles_AccessLevelId",
                table: "tblAccessLevel_Roles",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAccessLevel_Roles_RoleId",
                table: "tblAccessLevel_Roles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAccessLevel_Roles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96426a38-a89f-4091-a313-acc9017e6a71"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("e11cd3aa-c497-4f9d-8ed3-acc90008e9e7"), "0a9c31c0-f296-4aee-9519-7b40496372ee", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }
    }
}
