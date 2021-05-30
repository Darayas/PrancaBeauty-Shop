using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mi5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LangId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LangId",
                table: "AspNetUsers",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tblLanguages_LangId",
                table: "AspNetUsers",
                column: "LangId",
                principalTable: "tblLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblLanguages_LangId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LangId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LangId",
                table: "AspNetUsers");
        }
    }
}
