using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class Mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionFreeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionFreeItems",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductCategory",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductKeyword",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProducts",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionFreeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionFreeItems",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductCategory",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductKeyword",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProducts",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
