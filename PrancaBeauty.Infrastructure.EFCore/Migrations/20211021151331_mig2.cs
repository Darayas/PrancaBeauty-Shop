using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TopicId",
                table: "tblProducts",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_TopicId",
                table: "tblProducts",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblProductTopic_TopicId",
                table: "tblProducts",
                column: "TopicId",
                principalTable: "tblProductTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblProductTopic_TopicId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_TopicId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "tblProducts");
        }
    }
}
