using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: new Guid("1f55de17-d88a-4853-9604-acf3015150e7"));

            migrationBuilder.AlterColumn<string>(
                name: "Abbr",
                table: "tblLanguages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseForSiteLanguage",
                table: "tblLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
            //    values: new object[] { new Guid("833fc9bd-fc50-415d-8447-acf301549c1b"), "d611c921-01af-46cf-8a78-90807b2cab3c", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("833fc9bd-fc50-415d-8447-acf301549c1b"));

            migrationBuilder.DropColumn(
                name: "UseForSiteLanguage",
                table: "tblLanguages");

            migrationBuilder.AlterColumn<string>(
                name: "Abbr",
                table: "tblLanguages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("1f55de17-d88a-4853-9604-acf3015150e7"), "d87d9b12-8719-4b17-9271-7d9784f2b4c8", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }
    }
}
