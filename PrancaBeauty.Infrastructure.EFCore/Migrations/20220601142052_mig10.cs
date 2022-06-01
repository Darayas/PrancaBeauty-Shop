using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    VariantItemId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCarts_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCarts_tblProductVariantItems_VariantItemId",
                        column: x => x.VariantItemId,
                        principalTable: "tblProductVariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCarts_tblSellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "tblSellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCarts_ProductId",
                table: "tblCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarts_SellerId",
                table: "tblCarts",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarts_UserId",
                table: "tblCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarts_VariantItemId",
                table: "tblCarts",
                column: "VariantItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCarts");
        }
    }
}
