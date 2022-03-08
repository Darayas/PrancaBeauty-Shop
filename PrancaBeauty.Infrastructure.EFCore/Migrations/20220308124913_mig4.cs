using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblProductReviewsMedia_FileId",
                table: "tblProductReviewsMedia");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsMedia_FileId",
                table: "tblProductReviewsMedia",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblProductReviewsMedia_FileId",
                table: "tblProductReviewsMedia");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsMedia_FileId",
                table: "tblProductReviewsMedia",
                column: "FileId",
                unique: true);
        }
    }
}
