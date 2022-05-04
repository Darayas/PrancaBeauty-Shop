using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class Mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblProductMedia_FileId",
                table: "tblProductMedia");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductMedia_FileId",
                table: "tblProductMedia",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblProductMedia_FileId",
                table: "tblProductMedia");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductMedia_FileId",
                table: "tblProductMedia",
                column: "FileId",
                unique: true);
        }
    }
}
