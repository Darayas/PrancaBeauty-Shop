using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "tblProductPrices",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tblProductPrices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "tblLanguages",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tblCurrencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCurrencies_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCurrency_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCurrency_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCurrency_Translates_tblCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "tblCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCurrency_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPrices_CurrencyId",
                table: "tblProductPrices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLanguages_CountryId",
                table: "tblLanguages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCurrencies_CountryId",
                table: "tblCurrencies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCurrency_Translates_CurrencyId",
                table: "tblCurrency_Translates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCurrency_Translates_LangId",
                table: "tblCurrency_Translates",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLanguages_tblCountries_CountryId",
                table: "tblLanguages",
                column: "CountryId",
                principalTable: "tblCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductPrices_tblCurrencies_CurrencyId",
                table: "tblProductPrices",
                column: "CurrencyId",
                principalTable: "tblCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblLanguages_tblCountries_CountryId",
                table: "tblLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProductPrices_tblCurrencies_CurrencyId",
                table: "tblProductPrices");

            migrationBuilder.DropTable(
                name: "tblCurrency_Translates");

            migrationBuilder.DropTable(
                name: "tblCurrencies");

            migrationBuilder.DropIndex(
                name: "IX_tblProductPrices_CurrencyId",
                table: "tblProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_tblLanguages_CountryId",
                table: "tblLanguages");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "tblProductPrices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblProductPrices");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "tblLanguages");
        }
    }
}
