using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "tblAddress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "LangId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_CityId",
                table: "tblAddress",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_ProviceId",
                table: "tblAddress",
                column: "ProviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAddress_tblCities_CityId",
                table: "tblAddress",
                column: "CityId",
                principalTable: "tblCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAddress_tblProvinces_ProviceId",
                table: "tblAddress",
                column: "ProviceId",
                principalTable: "tblProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAddress_tblCities_CityId",
                table: "tblAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_tblAddress_tblProvinces_ProviceId",
                table: "tblAddress");

            migrationBuilder.DropIndex(
                name: "IX_tblAddress_CityId",
                table: "tblAddress");

            migrationBuilder.DropIndex(
                name: "IX_tblAddress_ProviceId",
                table: "tblAddress");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "tblAddress");

            migrationBuilder.AlterColumn<Guid>(
                name: "LangId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
