using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblShowcases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BackgroundColorCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CssStyle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CssClass = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsFullWidth = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShowcases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShowcases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShowcases_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblShowcasesTranslates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShowcasesTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShowcasesTranslates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShowcasesTranslates_tblShowcases_ShowcaseId",
                        column: x => x.ShowcaseId,
                        principalTable: "tblShowcases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblShowcaseTabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BackgroundColorCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShowcaseTabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShowcaseTabs_tblShowcases_ShowcaseId",
                        column: x => x.ShowcaseId,
                        principalTable: "tblShowcases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblShowcaseTabSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    ShowcaseTabId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    XlSize = table.Column<int>(type: "int", nullable: false),
                    LgSize = table.Column<int>(type: "int", nullable: false),
                    MdSize = table.Column<int>(type: "int", nullable: false),
                    SmSize = table.Column<int>(type: "int", nullable: false),
                    XsSize = table.Column<int>(type: "int", nullable: false),
                    IsSlider = table.Column<bool>(type: "bit", nullable: false),
                    CountInSection = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShowcaseTabSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShowcaseTabSections_tblShowcaseTabs_ShowcaseTabId",
                        column: x => x.ShowcaseTabId,
                        principalTable: "tblShowcaseTabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShowcaseTabSections_tblShowcaseTabSections_ParentId",
                        column: x => x.ParentId,
                        principalTable: "tblShowcaseTabSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblShowcaseTabTranslates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseTabId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShowcaseTabTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShowcaseTabTranslates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShowcaseTabTranslates_tblShowcaseTabs_ShowcaseTabId",
                        column: x => x.ShowcaseTabId,
                        principalTable: "tblShowcaseTabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSectionFreeItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseTabSectionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSectionFreeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSectionFreeItems_tblShowcaseTabSections_ShowcaseTabSectionId",
                        column: x => x.ShowcaseTabSectionId,
                        principalTable: "tblShowcaseTabSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSectionProductCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseTabSectionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountFetch = table.Column<int>(type: "int", nullable: false),
                    OrderBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSectionProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSectionProductCategory_tblCategoris_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSectionProductCategory_tblShowcaseTabSections_ShowcaseTabSectionId",
                        column: x => x.ShowcaseTabSectionId,
                        principalTable: "tblShowcaseTabSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSectionProductKeyword",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseTabSectionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    KeywordId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountFetch = table.Column<int>(type: "int", nullable: false),
                    OrderBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSectionProductKeyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSectionProductKeyword_tblKeywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "tblKeywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSectionProductKeyword_tblShowcaseTabSections_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "tblShowcaseTabSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSectionProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShowcaseTabSectionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSectionProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSectionProducts_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSectionProducts_tblShowcaseTabSections_ShowcaseTabSectionId",
                        column: x => x.ShowcaseTabSectionId,
                        principalTable: "tblShowcaseTabSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSectionFreeItemTranslate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SectionFreeItemId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSectionFreeItemTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSectionFreeItemTranslate_tblFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSectionFreeItemTranslate_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSectionFreeItemTranslate_tblSectionFreeItems_SectionFreeItemId",
                        column: x => x.SectionFreeItemId,
                        principalTable: "tblSectionFreeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionFreeItems_ShowcaseTabSectionId",
                table: "tblSectionFreeItems",
                column: "ShowcaseTabSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionFreeItemTranslate_FileId",
                table: "tblSectionFreeItemTranslate",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionFreeItemTranslate_LangId",
                table: "tblSectionFreeItemTranslate",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionFreeItemTranslate_SectionFreeItemId",
                table: "tblSectionFreeItemTranslate",
                column: "SectionFreeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProductCategory_CategoryId",
                table: "tblSectionProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProductCategory_ShowcaseTabSectionId",
                table: "tblSectionProductCategory",
                column: "ShowcaseTabSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProductKeyword_KeywordId",
                table: "tblSectionProductKeyword",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProducts_ProductId",
                table: "tblSectionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProducts_ShowcaseTabSectionId",
                table: "tblSectionProducts",
                column: "ShowcaseTabSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcases_CountryId",
                table: "tblShowcases",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcases_UserId",
                table: "tblShowcases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcasesTranslates_LangId",
                table: "tblShowcasesTranslates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcasesTranslates_ShowcaseId",
                table: "tblShowcasesTranslates",
                column: "ShowcaseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcaseTabs_ShowcaseId",
                table: "tblShowcaseTabs",
                column: "ShowcaseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcaseTabSections_ParentId",
                table: "tblShowcaseTabSections",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcaseTabSections_ShowcaseTabId",
                table: "tblShowcaseTabSections",
                column: "ShowcaseTabId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcaseTabTranslates_LangId",
                table: "tblShowcaseTabTranslates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcaseTabTranslates_ShowcaseTabId",
                table: "tblShowcaseTabTranslates",
                column: "ShowcaseTabId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSectionFreeItemTranslate");

            migrationBuilder.DropTable(
                name: "tblSectionProductCategory");

            migrationBuilder.DropTable(
                name: "tblSectionProductKeyword");

            migrationBuilder.DropTable(
                name: "tblSectionProducts");

            migrationBuilder.DropTable(
                name: "tblShowcasesTranslates");

            migrationBuilder.DropTable(
                name: "tblShowcaseTabTranslates");

            migrationBuilder.DropTable(
                name: "tblSectionFreeItems");

            migrationBuilder.DropTable(
                name: "tblShowcaseTabSections");

            migrationBuilder.DropTable(
                name: "tblShowcaseTabs");

            migrationBuilder.DropTable(
                name: "tblShowcases");
        }
    }
}
