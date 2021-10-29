using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guarantee",
                table: "tblProductSellers");

            migrationBuilder.DropColumn(
                name: "SendFrom",
                table: "tblProductSellers");

            migrationBuilder.AddColumn<int>(
                name: "CountInStock",
                table: "tblProductVariantItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "GuaranteeId",
                table: "tblProductVariantItems",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "tblProductVariantItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "tblProductVariantItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SendBy",
                table: "tblProductVariantItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SendFrom",
                table: "tblProductVariantItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tblGuarantee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGuarantee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGuarantee_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    GuaranteeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGuarantee_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGuarantee_Translates_tblGuarantee_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "tblGuarantee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblGuarantee_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariantItems_GuaranteeId",
                table: "tblProductVariantItems",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGuarantee_Translates_GuaranteeId",
                table: "tblGuarantee_Translates",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGuarantee_Translates_LangId",
                table: "tblGuarantee_Translates",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductVariantItems_tblGuarantee_GuaranteeId",
                table: "tblProductVariantItems",
                column: "GuaranteeId",
                principalTable: "tblGuarantee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductVariantItems_tblGuarantee_GuaranteeId",
                table: "tblProductVariantItems");

            migrationBuilder.DropTable(
                name: "tblGuarantee_Translates");

            migrationBuilder.DropTable(
                name: "tblGuarantee");

            migrationBuilder.DropIndex(
                name: "IX_tblProductVariantItems_GuaranteeId",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "CountInStock",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "GuaranteeId",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "SendBy",
                table: "tblProductVariantItems");

            migrationBuilder.DropColumn(
                name: "SendFrom",
                table: "tblProductVariantItems");

            migrationBuilder.AddColumn<string>(
                name: "Guarantee",
                table: "tblProductSellers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendFrom",
                table: "tblProductSellers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
