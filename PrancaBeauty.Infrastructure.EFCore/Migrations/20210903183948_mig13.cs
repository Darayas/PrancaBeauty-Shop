using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblLanguages_tblLanguageId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_tblLanguageId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "tblLanguageId",
                table: "tblProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImgId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_LangId",
                table: "tblProducts",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImgId",
                table: "AspNetUsers",
                column: "ProfileImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tblFiles_ProfileImgId",
                table: "AspNetUsers",
                column: "ProfileImgId",
                principalTable: "tblFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblLanguages_LangId",
                table: "tblProducts",
                column: "LangId",
                principalTable: "tblLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tblFiles_ProfileImgId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblLanguages_LangId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_LangId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImgId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImgId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "tblLanguageId",
                table: "tblProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_tblLanguageId",
                table: "tblProducts",
                column: "tblLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblLanguages_tblLanguageId",
                table: "tblProducts",
                column: "tblLanguageId",
                principalTable: "tblLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
