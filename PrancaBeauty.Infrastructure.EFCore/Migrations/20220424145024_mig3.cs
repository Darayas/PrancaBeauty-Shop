using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSections_ShowcaseTabSectionId",
                table: "tblSectionFreeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSections_ShowcaseTabSectionId",
                table: "tblSectionProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSections_KeywordId",
                table: "tblSectionProductKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSections_ShowcaseTabSectionId",
                table: "tblSectionProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionProducts_ShowcaseTabSectionId",
                table: "tblSectionProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionProductCategory_ShowcaseTabSectionId",
                table: "tblSectionProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionFreeItems_ShowcaseTabSectionId",
                table: "tblSectionFreeItems");

            migrationBuilder.RenameColumn(
                name: "ShowcaseTabSectionId",
                table: "tblSectionProducts",
                newName: "TabSectionItemId");

            migrationBuilder.RenameColumn(
                name: "ShowcaseTabSectionId",
                table: "tblSectionProductKeyword",
                newName: "TabSectionItemId");

            migrationBuilder.RenameColumn(
                name: "ShowcaseTabSectionId",
                table: "tblSectionProductCategory",
                newName: "TabSectionItemId");

            migrationBuilder.RenameColumn(
                name: "ShowcaseTabSectionId",
                table: "tblSectionFreeItems",
                newName: "TabSectionItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tblShowcasesTranslates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tblShowcasesTranslates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tblShowcaseTabSectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    TabSectionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    SectionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShowcaseTabSectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShowcaseTabSectionItems_tblShowcaseTabSections_TabSectionId",
                        column: x => x.TabSectionId,
                        principalTable: "tblShowcaseTabSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProducts_TabSectionItemId",
                table: "tblSectionProducts",
                column: "TabSectionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProductKeyword_TabSectionItemId",
                table: "tblSectionProductKeyword",
                column: "TabSectionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProductCategory_TabSectionItemId",
                table: "tblSectionProductCategory",
                column: "TabSectionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionFreeItems_TabSectionItemId",
                table: "tblSectionFreeItems",
                column: "TabSectionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblShowcaseTabSectionItems_TabSectionId",
                table: "tblShowcaseTabSectionItems",
                column: "TabSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionFreeItems",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductCategory",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductKeyword",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProducts",
                column: "TabSectionItemId",
                principalTable: "tblShowcaseTabSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionFreeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProductKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSectionItems_TabSectionItemId",
                table: "tblSectionProducts");

            migrationBuilder.DropTable(
                name: "tblShowcaseTabSectionItems");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionProducts_TabSectionItemId",
                table: "tblSectionProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionProductKeyword_TabSectionItemId",
                table: "tblSectionProductKeyword");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionProductCategory_TabSectionItemId",
                table: "tblSectionProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblSectionFreeItems_TabSectionItemId",
                table: "tblSectionFreeItems");

            migrationBuilder.RenameColumn(
                name: "TabSectionItemId",
                table: "tblSectionProducts",
                newName: "ShowcaseTabSectionId");

            migrationBuilder.RenameColumn(
                name: "TabSectionItemId",
                table: "tblSectionProductKeyword",
                newName: "ShowcaseTabSectionId");

            migrationBuilder.RenameColumn(
                name: "TabSectionItemId",
                table: "tblSectionProductCategory",
                newName: "ShowcaseTabSectionId");

            migrationBuilder.RenameColumn(
                name: "TabSectionItemId",
                table: "tblSectionFreeItems",
                newName: "ShowcaseTabSectionId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tblShowcasesTranslates",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tblShowcasesTranslates",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProducts_ShowcaseTabSectionId",
                table: "tblSectionProducts",
                column: "ShowcaseTabSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionProductCategory_ShowcaseTabSectionId",
                table: "tblSectionProductCategory",
                column: "ShowcaseTabSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSectionFreeItems_ShowcaseTabSectionId",
                table: "tblSectionFreeItems",
                column: "ShowcaseTabSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionFreeItems_tblShowcaseTabSections_ShowcaseTabSectionId",
                table: "tblSectionFreeItems",
                column: "ShowcaseTabSectionId",
                principalTable: "tblShowcaseTabSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductCategory_tblShowcaseTabSections_ShowcaseTabSectionId",
                table: "tblSectionProductCategory",
                column: "ShowcaseTabSectionId",
                principalTable: "tblShowcaseTabSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProductKeyword_tblShowcaseTabSections_KeywordId",
                table: "tblSectionProductKeyword",
                column: "KeywordId",
                principalTable: "tblShowcaseTabSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSectionProducts_tblShowcaseTabSections_ShowcaseTabSectionId",
                table: "tblSectionProducts",
                column: "ShowcaseTabSectionId",
                principalTable: "tblShowcaseTabSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
