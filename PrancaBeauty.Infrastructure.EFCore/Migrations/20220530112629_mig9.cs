using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TopicId",
                table: "tblCategoris",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoris_TopicId",
                table: "tblCategoris",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategoris_tblProductTopic_TopicId",
                table: "tblCategoris",
                column: "TopicId",
                principalTable: "tblProductTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategoris_tblProductTopic_TopicId",
                table: "tblCategoris");

            migrationBuilder.DropIndex(
                name: "IX_tblCategoris_TopicId",
                table: "tblCategoris");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "tblCategoris");
        }
    }
}
