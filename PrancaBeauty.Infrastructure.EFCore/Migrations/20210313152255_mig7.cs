using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: new Guid("af2b2027-7420-4926-81b9-acd501786fbf"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTrySentSms",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordPhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
            //    values: new object[] { new Guid("2cc4e394-21f1-4cd8-bdf5-acea013729c3"), "7ad5f99a-372d-464d-b34f-a0e518eb191c", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2cc4e394-21f1-4cd8-bdf5-acea013729c3"));

            migrationBuilder.DropColumn(
                name: "LastTrySentSms",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordPhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("af2b2027-7420-4926-81b9-acd501786fbf"), "d35d7651-0cf3-407f-984e-8f4c11ff162c", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }
    }
}
