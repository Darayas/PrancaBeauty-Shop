using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "aDec",
                table: "tblCurrencies",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: ".");

            migrationBuilder.AddColumn<string>(
                name: "aSep",
                table: "tblCurrencies",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: ",");

            migrationBuilder.AddColumn<string>(
                name: "mDec",
                table: "tblCurrencies",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "0");

            migrationBuilder.AddColumn<string>(
                name: "vMax",
                table: "tblCurrencies",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "9999999999");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aDec",
                table: "tblCurrencies");

            migrationBuilder.DropColumn(
                name: "aSep",
                table: "tblCurrencies");

            migrationBuilder.DropColumn(
                name: "mDec",
                table: "tblCurrencies");

            migrationBuilder.DropColumn(
                name: "vMax",
                table: "tblCurrencies");
        }
    }
}
