using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblProvinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProvinces_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_tblProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProvinces_Translate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProvinces_Translate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProvinces_Translate_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProvinces_Translate_tblProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCities_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_Translates_tblCities_CityId",
                        column: x => x.CityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCities_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCountries_Translates_LangId",
                table: "tblCountries_Translates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_ProvinceId",
                table: "tblCities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_Translates_CityId",
                table: "tblCities_Translates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_Translates_LangId",
                table: "tblCities_Translates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvinces_CountryId",
                table: "tblProvinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvinces_Translate_LangId",
                table: "tblProvinces_Translate",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvinces_Translate_ProvinceId",
                table: "tblProvinces_Translate",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCountries_Translates_tblLanguages_LangId",
                table: "tblCountries_Translates",
                column: "LangId",
                principalTable: "tblLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCountries_Translates_tblLanguages_LangId",
                table: "tblCountries_Translates");

            migrationBuilder.DropTable(
                name: "tblCities_Translates");

            migrationBuilder.DropTable(
                name: "tblProvinces_Translate");

            migrationBuilder.DropTable(
                name: "tblCities");

            migrationBuilder.DropTable(
                name: "tblProvinces");

            migrationBuilder.DropIndex(
                name: "IX_tblCountries_Translates_LangId",
                table: "tblCountries_Translates");
        }
    }
}
