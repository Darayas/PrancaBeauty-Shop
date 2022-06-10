using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class Mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "tblWallets",
                type: "uniqueidentifier",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "tblWallets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tblShippingMethodRestricts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "tblWallets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblShippingMethodRestricts");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "tblWallets",
                type: "uniqueidentifier",
                maxLength: 450,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 450,
                oldNullable: true);
        }
    }
}
