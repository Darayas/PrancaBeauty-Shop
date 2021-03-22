using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: new Guid("2cc4e394-21f1-4cd8-bdf5-acea013729c3"));

            migrationBuilder.AddColumn<string>(
                name: "Abbr",
                table: "tblLanguages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
            //    values: new object[] { new Guid("1f55de17-d88a-4853-9604-acf3015150e7"), "d87d9b12-8719-4b17-9271-7d9784f2b4c8", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1f55de17-d88a-4853-9604-acf3015150e7"));

            migrationBuilder.DropColumn(
                name: "Abbr",
                table: "tblLanguages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("2cc4e394-21f1-4cd8-bdf5-acea013729c3"), "7ad5f99a-372d-464d-b34f-a0e518eb191c", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }
    }
}
