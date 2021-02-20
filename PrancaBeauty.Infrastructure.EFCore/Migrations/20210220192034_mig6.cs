using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: new Guid("5580d6ae-db1a-4187-8ddc-acd201866a60"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "tblSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "tblSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
            //    values: new object[] { new Guid("af2b2027-7420-4926-81b9-acd501786fbf"), "d35d7651-0cf3-407f-984e-8f4c11ff162c", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af2b2027-7420-4926-81b9-acd501786fbf"));

            migrationBuilder.DropColumn(
                name: "Date",
                table: "tblSettings");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "tblSettings");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("5580d6ae-db1a-4187-8ddc-acd201866a60"), "85e53b1c-4877-4150-b2ee-4b2c6bf387b8", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }
    }
}
