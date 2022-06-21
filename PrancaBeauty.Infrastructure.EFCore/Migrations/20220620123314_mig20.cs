using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblSellers_AddressId",
                table: "tblSellers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "tblSellers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSellers_AddressId",
                table: "tblSellers",
                column: "AddressId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblSellers_AddressId",
                table: "tblSellers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "tblSellers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_tblSellers_AddressId",
                table: "tblSellers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");
        }
    }
}
