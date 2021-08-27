using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "tblProductSellers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "tblProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "tblProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "tblProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LangId",
                table: "tblProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UniqueNumber",
                table: "tblProducts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

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
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk",
                column: "AskId",
                principalTable: "tblProductAsk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblLanguages_tblLanguageId",
                table: "tblProducts",
                column: "tblLanguageId",
                principalTable: "tblLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblLanguages_tblLanguageId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_tblLanguageId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "tblProductSellers");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "LangId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "UniqueNumber",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "tblLanguageId",
                table: "tblProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductAsk_tblProductAsk_AskId",
                table: "tblProductAsk",
                column: "AskId",
                principalTable: "tblProductAsk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
