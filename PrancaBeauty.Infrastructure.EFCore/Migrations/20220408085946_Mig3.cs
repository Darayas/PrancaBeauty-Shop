using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class Mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DiscountId",
                table: "tblProductVariantItems",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariantItems_DiscountId",
                table: "tblProductVariantItems",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductVariantItems_tblProductDiscounts_DiscountId",
                table: "tblProductVariantItems",
                column: "DiscountId",
                principalTable: "tblProductDiscounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductVariantItems_tblProductDiscounts_DiscountId",
                table: "tblProductVariantItems");

            migrationBuilder.DropIndex(
                name: "IX_tblProductVariantItems_DiscountId",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "tblProductVariantItems");
        }
    }
}
