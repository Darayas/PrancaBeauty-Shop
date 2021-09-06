using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblLanguages_tblFiles_FlagImgId",
                table: "tblLanguages");

            migrationBuilder.DropIndex(
                name: "IX_tblLanguages_FlagImgId",
                table: "tblLanguages");

            migrationBuilder.DropColumn(
                name: "FlagImgId",
                table: "tblLanguages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FlagImgId",
                table: "tblLanguages",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tblLanguages_FlagImgId",
                table: "tblLanguages",
                column: "FlagImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLanguages_tblFiles_FlagImgId",
                table: "tblLanguages",
                column: "FlagImgId",
                principalTable: "tblFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
