using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "tblProductSellers");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "tblProductVariantItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "tblProductVariantItems");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "tblProductSellers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
