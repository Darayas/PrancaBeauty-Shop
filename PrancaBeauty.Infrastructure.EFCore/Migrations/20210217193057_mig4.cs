using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: new Guid("96426a38-a89f-4091-a313-acc9017e6a71"));

            //migrationBuilder.CreateTable(
            //    name: "tblLanguages",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        NativeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        IsRtl = table.Column<bool>(type: "bit", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblLanguages", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblTamplates",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
            //        LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Template = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblTamplates", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_tblTamplates_tblLanguages_LangId",
            //            column: x => x.LangId,
            //            principalTable: "tblLanguages",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
            //    values: new object[] { new Guid("b2683ce0-eb78-426d-8b09-acd2017b4982"), "d44d3106-ac6b-4558-9791-ed0d1bff15b6", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });

            //migrationBuilder.CreateIndex(
            //    name: "IX_tblTamplates_LangId",
            //    table: "tblTamplates",
            //    column: "LangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTamplates");

            migrationBuilder.DropTable(
                name: "tblLanguages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b2683ce0-eb78-426d-8b09-acd2017b4982"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PageName", "ParentId", "Sort" },
                values: new object[] { new Guid("96426a38-a89f-4091-a313-acc9017e6a71"), "180eb0ed-35c4-4be4-8023-a0cf4f0081fb", "دسترسی مدیر کل", "FullControl", "FULLCONTROL", "FullControl", null, 0 });
        }
    }
}
