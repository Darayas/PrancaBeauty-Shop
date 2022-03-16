using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "tblProductAskLikes");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "tblProductAskLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "tblProductAskLikes");

            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "tblProductAskLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
