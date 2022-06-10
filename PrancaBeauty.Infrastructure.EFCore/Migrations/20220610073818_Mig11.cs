using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class Mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaxGroupId",
                table: "tblProducts",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "tblTaxGroupsId",
                table: "tblProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblBankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    BankName = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBankAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentGates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentGates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPaymentGates_tblFiles_LogoId",
                        column: x => x.LogoId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductGroups_tblProductTopic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "tblProductTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblShippingMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShippingMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShippingMethods_tblFiles_LogoId",
                        column: x => x.LogoId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTaxGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Percent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTaxGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTaxGroups_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWallets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblWallets_tblCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "tblCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblBills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    GateId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    BillNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaxAmount = table.Column<double>(type: "float", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    TransactionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBills_tblPaymentGates_GateId",
                        column: x => x.GateId,
                        principalTable: "tblPaymentGates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentGateRestricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    PaymentGateId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentGateRestricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPaymentGateRestricts_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPaymentGateRestricts_tblCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "tblCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPaymentGateRestricts_tblPaymentGates_PaymentGateId",
                        column: x => x.PaymentGateId,
                        principalTable: "tblPaymentGates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentGateTranslate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    PaymentGateId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentGateTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPaymentGateTranslate_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPaymentGateTranslate_tblPaymentGates_PaymentGateId",
                        column: x => x.PaymentGateId,
                        principalTable: "tblPaymentGates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductGroupTranslate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductGroupId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductGroupTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductGroupTranslate_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductGroupTranslate_tblProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "tblProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblShippingMethodRestricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShippingMethodId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShippingMethodRestricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShippingMethodRestricts_tblCities_CityId",
                        column: x => x.CityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShippingMethodRestricts_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShippingMethodRestricts_tblProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShippingMethodRestricts_tblShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "tblShippingMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblShippingMethodTranslate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShippingMethodId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShippingMethodTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShippingMethodTranslate_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblShippingMethodTranslate_tblShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "tblShippingMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductGroupPercents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductGroupId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Percent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductGroupPercents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductGroupPercents_tblProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "tblProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductGroupPercents_tblWallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "tblWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblWalletDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    DetailsType = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWalletDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWalletDetails_tblWallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "tblWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPostalBarcodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ShippingMethodId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangeStatusDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPostalBarcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPostalBarcodes_tblAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "tblAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPostalBarcodes_tblBills_BillId",
                        column: x => x.BillId,
                        principalTable: "tblBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPostalBarcodes_tblShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "tblShippingMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblWalletProductDepositDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    WalletDetailsId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    VariantItemId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWalletProductDepositDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWalletProductDepositDetails_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblWalletProductDepositDetails_tblProductVariantItems_VariantItemId",
                        column: x => x.VariantItemId,
                        principalTable: "tblProductVariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblWalletProductDepositDetails_tblWalletDetails_WalletDetailsId",
                        column: x => x.WalletDetailsId,
                        principalTable: "tblWalletDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblWalletWithdraw",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    WalletDetailsId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWalletWithdraw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWalletWithdraw_tblBankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "tblBankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblWalletWithdraw_tblWalletDetails_WalletDetailsId",
                        column: x => x.WalletDetailsId,
                        principalTable: "tblWalletDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblBillItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    VarianrItemId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    TaxGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostalBarcodeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    TaxPercent = table.Column<double>(type: "float", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    ReturnStatus = table.Column<int>(type: "int", nullable: false),
                    ReasonReturn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    tblTaxGroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBillItems_tblBills_BillId",
                        column: x => x.BillId,
                        principalTable: "tblBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBillItems_tblPostalBarcodes_PostalBarcodeId",
                        column: x => x.PostalBarcodeId,
                        principalTable: "tblPostalBarcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBillItems_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBillItems_tblProductVariantItems_VarianrItemId",
                        column: x => x.VarianrItemId,
                        principalTable: "tblProductVariantItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBillItems_tblSellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "tblSellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBillItems_tblTaxGroups_tblTaxGroupsId",
                        column: x => x.tblTaxGroupsId,
                        principalTable: "tblTaxGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_tblTaxGroupsId",
                table: "tblProducts",
                column: "tblTaxGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBankAccounts_UserId",
                table: "tblBankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillItems_BillId",
                table: "tblBillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillItems_PostalBarcodeId",
                table: "tblBillItems",
                column: "PostalBarcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillItems_ProductId",
                table: "tblBillItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillItems_SellerId",
                table: "tblBillItems",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillItems_tblTaxGroupsId",
                table: "tblBillItems",
                column: "tblTaxGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBillItems_VarianrItemId",
                table: "tblBillItems",
                column: "VarianrItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBills_GateId",
                table: "tblBills",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBills_UserId",
                table: "tblBills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentGateRestricts_CountryId",
                table: "tblPaymentGateRestricts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentGateRestricts_CurrencyId",
                table: "tblPaymentGateRestricts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentGateRestricts_PaymentGateId",
                table: "tblPaymentGateRestricts",
                column: "PaymentGateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentGates_LogoId",
                table: "tblPaymentGates",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentGateTranslate_LangId",
                table: "tblPaymentGateTranslate",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentGateTranslate_PaymentGateId",
                table: "tblPaymentGateTranslate",
                column: "PaymentGateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPostalBarcodes_AddressId",
                table: "tblPostalBarcodes",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPostalBarcodes_BillId",
                table: "tblPostalBarcodes",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPostalBarcodes_ShippingMethodId",
                table: "tblPostalBarcodes",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductGroupPercents_ProductGroupId",
                table: "tblProductGroupPercents",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductGroupPercents_WalletId",
                table: "tblProductGroupPercents",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductGroups_TopicId",
                table: "tblProductGroups",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductGroupTranslate_LangId",
                table: "tblProductGroupTranslate",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductGroupTranslate_ProductGroupId",
                table: "tblProductGroupTranslate",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethodRestricts_CityId",
                table: "tblShippingMethodRestricts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethodRestricts_CountryId",
                table: "tblShippingMethodRestricts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethodRestricts_ProvinceId",
                table: "tblShippingMethodRestricts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethodRestricts_ShippingMethodId",
                table: "tblShippingMethodRestricts",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethods_LogoId",
                table: "tblShippingMethods",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethodTranslate_LangId",
                table: "tblShippingMethodTranslate",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShippingMethodTranslate_ShippingMethodId",
                table: "tblShippingMethodTranslate",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTaxGroups_CountryId",
                table: "tblTaxGroups",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWalletDetails_WalletId",
                table: "tblWalletDetails",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWalletProductDepositDetails_ProductId",
                table: "tblWalletProductDepositDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWalletProductDepositDetails_VariantItemId",
                table: "tblWalletProductDepositDetails",
                column: "VariantItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWalletProductDepositDetails_WalletDetailsId",
                table: "tblWalletProductDepositDetails",
                column: "WalletDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWallets_CurrencyId",
                table: "tblWallets",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWallets_UserId",
                table: "tblWallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWalletWithdraw_BankAccountId",
                table: "tblWalletWithdraw",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWalletWithdraw_WalletDetailsId",
                table: "tblWalletWithdraw",
                column: "WalletDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblTaxGroups_tblTaxGroupsId",
                table: "tblProducts",
                column: "tblTaxGroupsId",
                principalTable: "tblTaxGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblTaxGroups_tblTaxGroupsId",
                table: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblBillItems");

            migrationBuilder.DropTable(
                name: "tblPaymentGateRestricts");

            migrationBuilder.DropTable(
                name: "tblPaymentGateTranslate");

            migrationBuilder.DropTable(
                name: "tblProductGroupPercents");

            migrationBuilder.DropTable(
                name: "tblProductGroupTranslate");

            migrationBuilder.DropTable(
                name: "tblShippingMethodRestricts");

            migrationBuilder.DropTable(
                name: "tblShippingMethodTranslate");

            migrationBuilder.DropTable(
                name: "tblWalletProductDepositDetails");

            migrationBuilder.DropTable(
                name: "tblWalletWithdraw");

            migrationBuilder.DropTable(
                name: "tblPostalBarcodes");

            migrationBuilder.DropTable(
                name: "tblTaxGroups");

            migrationBuilder.DropTable(
                name: "tblProductGroups");

            migrationBuilder.DropTable(
                name: "tblBankAccounts");

            migrationBuilder.DropTable(
                name: "tblWalletDetails");

            migrationBuilder.DropTable(
                name: "tblBills");

            migrationBuilder.DropTable(
                name: "tblShippingMethods");

            migrationBuilder.DropTable(
                name: "tblWallets");

            migrationBuilder.DropTable(
                name: "tblPaymentGates");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_tblTaxGroupsId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "TaxGroupId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "tblTaxGroupsId",
                table: "tblProducts");
        }
    }
}
