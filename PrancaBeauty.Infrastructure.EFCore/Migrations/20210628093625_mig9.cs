using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FontIconCode",
                table: "tblCategoris");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "tblCategoris",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoris_ImageId",
                table: "tblCategoris",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategoris_tblFiles_ImageId",
                table: "tblCategoris",
                column: "ImageId",
                principalTable: "tblFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategoris_tblFiles_ImageId",
                table: "tblCategoris");

            migrationBuilder.DropIndex(
                name: "IX_tblCategoris_ImageId",
                table: "tblCategoris");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "tblCategoris");

            migrationBuilder.AddColumn<string>(
                name: "FontIconCode",
                table: "tblCategoris",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
