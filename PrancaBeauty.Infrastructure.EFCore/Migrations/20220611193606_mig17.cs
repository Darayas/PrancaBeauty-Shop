using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPostalBarcodes_tblAddress_AddressId",
                table: "tblPostalBarcodes");

            migrationBuilder.DropIndex(
                name: "IX_tblPostalBarcodes_AddressId",
                table: "tblPostalBarcodes");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "tblPostalBarcodes");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "tblBills",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "tblBills",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBills_AddressId",
                table: "tblBills",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBills_tblAddress_AddressId",
                table: "tblBills",
                column: "AddressId",
                principalTable: "tblAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBills_tblAddress_AddressId",
                table: "tblBills");

            migrationBuilder.DropIndex(
                name: "IX_tblBills_AddressId",
                table: "tblBills");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "tblBills");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "tblBills");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "tblPostalBarcodes",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblPostalBarcodes_AddressId",
                table: "tblPostalBarcodes",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPostalBarcodes_tblAddress_AddressId",
                table: "tblPostalBarcodes",
                column: "AddressId",
                principalTable: "tblAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
