using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GateNumber",
                table: "tblBills",
                newName: "Authority");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "tblPaymentGates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "tblPaymentGates");

            migrationBuilder.RenameColumn(
                name: "Authority",
                table: "tblBills",
                newName: "GateNumber");
        }
    }
}
