using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk",
                column: "AskId",
                principalTable: "tblProductAsk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk",
                column: "AskId",
                principalTable: "tblProductAsk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
