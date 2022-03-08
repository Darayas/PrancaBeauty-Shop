using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "tblProductReviewsLikes");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "tblProductReviewsLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "tblProductReviewsLikes");

            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "tblProductReviewsLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
