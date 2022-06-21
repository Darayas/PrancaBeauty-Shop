using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "tblSellers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSellers_AddressId",
                table: "tblSellers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSellers_tblAddress_AddressId",
                table: "tblSellers",
                column: "AddressId",
                principalTable: "tblAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSellers_tblAddress_AddressId",
                table: "tblSellers");

            migrationBuilder.DropIndex(
                name: "IX_tblSellers_AddressId",
                table: "tblSellers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "tblSellers");
        }
    }
}
