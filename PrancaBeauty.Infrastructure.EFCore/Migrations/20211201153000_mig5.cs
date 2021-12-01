using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductSellers_AspNetUsers_SellerUserId",
                table: "tblProductSellers");

            migrationBuilder.RenameColumn(
                name: "SellerUserId",
                table: "tblProductSellers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblProductSellers_SellerUserId",
                table: "tblProductSellers",
                newName: "IX_tblProductSellers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductSellers_AspNetUsers_UserId",
                table: "tblProductSellers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductSellers_AspNetUsers_UserId",
                table: "tblProductSellers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblProductSellers",
                newName: "SellerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblProductSellers_UserId",
                table: "tblProductSellers",
                newName: "IX_tblProductSellers_SellerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductSellers_AspNetUsers_SellerUserId",
                table: "tblProductSellers",
                column: "SellerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
