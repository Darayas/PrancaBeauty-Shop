using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "tblProductReviewsAttributeValues",
                type: "float",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "tblProductReviewsAttributeValues",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 200);
        }
    }
}
