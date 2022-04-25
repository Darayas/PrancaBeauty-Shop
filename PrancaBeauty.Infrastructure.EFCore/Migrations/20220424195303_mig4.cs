using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "tblSectionProducts");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "tblSectionFreeItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "tblSectionProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "tblSectionFreeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
