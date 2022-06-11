using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShippingMethodId",
                table: "tblPostalBarcodes",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "tblPostalBarcodes",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShippingMethodId",
                table: "tblPostalBarcodes",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "tblPostalBarcodes",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
