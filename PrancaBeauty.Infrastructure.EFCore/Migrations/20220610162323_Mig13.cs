using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class Mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblTaxGroups_tblTaxGroupsId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_tblTaxGroupsId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "tblTaxGroupsId",
                table: "tblProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxGroupId",
                table: "tblProducts",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductGroupId",
                table: "tblProducts",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_ProductGroupId",
                table: "tblProducts",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_TaxGroupId",
                table: "tblProducts",
                column: "TaxGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblProductGroups_ProductGroupId",
                table: "tblProducts",
                column: "ProductGroupId",
                principalTable: "tblProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblTaxGroups_TaxGroupId",
                table: "tblProducts",
                column: "TaxGroupId",
                principalTable: "tblTaxGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblProductGroups_ProductGroupId",
                table: "tblProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblTaxGroups_TaxGroupId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_ProductGroupId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_TaxGroupId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "tblProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxGroupId",
                table: "tblProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tblTaxGroupsId",
                table: "tblProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_tblTaxGroupsId",
                table: "tblProducts",
                column: "tblTaxGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblTaxGroups_tblTaxGroupsId",
                table: "tblProducts",
                column: "tblTaxGroupsId",
                principalTable: "tblTaxGroups",
                principalColumn: "Id");
        }
    }
}
