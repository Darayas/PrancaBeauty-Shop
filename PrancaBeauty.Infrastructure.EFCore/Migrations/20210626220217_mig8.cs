using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblCategory_Translates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblCategory_Translates");
        }
    }
}
