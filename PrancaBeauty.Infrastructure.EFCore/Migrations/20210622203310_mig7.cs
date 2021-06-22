using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneCode",
                table: "tblCountries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tblCategoris",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    FontIconCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategoris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategoris_tblCategoris_ParentId",
                        column: x => x.ParentId,
                        principalTable: "tblCategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCategory_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategory_Translates_tblCategoris_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCategory_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoris_ParentId",
                table: "tblCategoris",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_Translates_CategoryId",
                table: "tblCategory_Translates",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_Translates_LangId",
                table: "tblCategory_Translates",
                column: "LangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCategory_Translates");

            migrationBuilder.DropTable(
                name: "tblCategoris");

            migrationBuilder.DropColumn(
                name: "PhoneCode",
                table: "tblCountries");
        }
    }
}
