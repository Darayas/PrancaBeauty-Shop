using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_Translates_tblCategoris_CategoryId",
                table: "tblCategory_Translates");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_Translates_tblCategoris_CategoryId",
                table: "tblCategory_Translates",
                column: "CategoryId",
                principalTable: "tblCategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_Translates_tblCategoris_CategoryId",
                table: "tblCategory_Translates");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_Translates_tblCategoris_CategoryId",
                table: "tblCategory_Translates",
                column: "CategoryId",
                principalTable: "tblCategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
