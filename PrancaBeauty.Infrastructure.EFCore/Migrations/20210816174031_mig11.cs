using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeller",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tblKeywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKeywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    AuthorUserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProducts_AspNetUsers_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategoris_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductTopic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductTopic_tblFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    VariantType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductVariants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSellers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblKeywords_Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    KeywordId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Similarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKeywords_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblKeywords_Products_tblKeywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "tblKeywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKeywords_Products_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductAsk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    AskId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductAsk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductAsk_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductAsk_tblProductAsk_AskId",
                        column: x => x.AskId,
                        principalTable: "tblProductAsk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductAsk_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductMedia_tblFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductMedia_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductPrices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductPrices_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductSellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SellerUserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Guarantee = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SendFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductSellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductSellers_AspNetUsers_SellerUserId",
                        column: x => x.SellerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductSellers_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductPropertis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductPropertis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductPropertis_tblProductTopic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "tblProductTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductReviewsAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductReviewsAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsAttribute_tblProductTopic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "tblProductTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductTopic_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductTopicId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductTopic_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductTopic_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductTopic_Translates_tblProductTopic_ProductTopicId",
                        column: x => x.ProductTopicId,
                        principalTable: "tblProductTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductVariants_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductVariants_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductVariants_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductVariants_Translates_tblProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "tblProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSeller_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSeller_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSeller_Translates_tblFiles_LogoId",
                        column: x => x.LogoId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSeller_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSeller_Translates_tblSellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "tblSellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductAskLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductAskId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductAskLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductAskLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductAskLikes_tblProductAsk_ProductAskId",
                        column: x => x.ProductAskId,
                        principalTable: "tblProductAsk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductSellerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    AuthorUserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountStar = table.Column<int>(type: "int", nullable: false),
                    Advantages = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DisAdvantages = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductReviews_AspNetUsers_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductReviews_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductReviews_tblProductSellers_ProductSellerId",
                        column: x => x.ProductSellerId,
                        principalTable: "tblProductSellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductVariantItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductSellerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Percent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductVariantItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductVariantItems_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductVariantItems_tblProductSellers_ProductSellerId",
                        column: x => x.ProductSellerId,
                        principalTable: "tblProductSellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductVariantItems_tblProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "tblProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductPropertiesValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductPropertiesId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductPropertiesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductPropertiesValues_tblProductPropertis_ProductPropertiesId",
                        column: x => x.ProductPropertiesId,
                        principalTable: "tblProductPropertis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductPropertiesValues_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductPropertis_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductPropertis_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductPropertis_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductPropertis_Translates_tblProductPropertis_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "tblProductPropertis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductReviewsAttribute_Translate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductReviewsAttributeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductReviewsAttribute_Translate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsAttribute_Translate_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsAttribute_Translate_tblProductReviewsAttribute_ProductReviewsAttributeId",
                        column: x => x.ProductReviewsAttributeId,
                        principalTable: "tblProductReviewsAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductReviewsAttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductReviewId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductReviewAttributeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductReviewsAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsAttributeValues_tblProductReviews_ProductReviewId",
                        column: x => x.ProductReviewId,
                        principalTable: "tblProductReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsAttributeValues_tblProductReviewsAttribute_ProductReviewAttributeId",
                        column: x => x.ProductReviewAttributeId,
                        principalTable: "tblProductReviewsAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProductReviewsLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductReviewId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductReviewsLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsLikes_tblProductReviews_ProductReviewId",
                        column: x => x.ProductReviewId,
                        principalTable: "tblProductReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductReviewsMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductReviewsId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductReviewsMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsMedia_tblFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductReviewsMedia_tblProductReviews_ProductReviewsId",
                        column: x => x.ProductReviewsId,
                        principalTable: "tblProductReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKeywords_Products_KeywordId",
                table: "tblKeywords_Products",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKeywords_Products_ProductId",
                table: "tblKeywords_Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductAsk_AskId",
                table: "tblProductAsk",
                column: "AskId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductAsk_ProductId",
                table: "tblProductAsk",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductAsk_UserId",
                table: "tblProductAsk",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductAskLikes_ProductAskId",
                table: "tblProductAskLikes",
                column: "ProductAskId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductAskLikes_UserId",
                table: "tblProductAskLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductMedia_FileId",
                table: "tblProductMedia",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProductMedia_ProductId",
                table: "tblProductMedia",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPrices_ProductId",
                table: "tblProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPrices_UserId",
                table: "tblProductPrices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPropertiesValues_ProductId",
                table: "tblProductPropertiesValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPropertiesValues_ProductPropertiesId",
                table: "tblProductPropertiesValues",
                column: "ProductPropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPropertis_TopicId",
                table: "tblProductPropertis",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPropertis_Translates_LangId",
                table: "tblProductPropertis_Translates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPropertis_Translates_PropertyId",
                table: "tblProductPropertis_Translates",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviews_AuthorUserId",
                table: "tblProductReviews",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviews_ProductId",
                table: "tblProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviews_ProductSellerId",
                table: "tblProductReviews",
                column: "ProductSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsAttribute_TopicId",
                table: "tblProductReviewsAttribute",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsAttribute_Translate_LangId",
                table: "tblProductReviewsAttribute_Translate",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsAttribute_Translate_ProductReviewsAttributeId",
                table: "tblProductReviewsAttribute_Translate",
                column: "ProductReviewsAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsAttributeValues_ProductReviewAttributeId",
                table: "tblProductReviewsAttributeValues",
                column: "ProductReviewAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsAttributeValues_ProductReviewId",
                table: "tblProductReviewsAttributeValues",
                column: "ProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsLikes_ProductReviewId",
                table: "tblProductReviewsLikes",
                column: "ProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsLikes_UserId",
                table: "tblProductReviewsLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsMedia_FileId",
                table: "tblProductReviewsMedia",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProductReviewsMedia_ProductReviewsId",
                table: "tblProductReviewsMedia",
                column: "ProductReviewsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_AuthorUserId",
                table: "tblProducts",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_CategoryId",
                table: "tblProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductSellers_ProductId",
                table: "tblProductSellers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductSellers_SellerUserId",
                table: "tblProductSellers",
                column: "SellerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductTopic_FileId",
                table: "tblProductTopic",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductTopic_Translates_LangId",
                table: "tblProductTopic_Translates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductTopic_Translates_ProductTopicId",
                table: "tblProductTopic_Translates",
                column: "ProductTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariantItems_ProductId",
                table: "tblProductVariantItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariantItems_ProductSellerId",
                table: "tblProductVariantItems",
                column: "ProductSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariantItems_ProductVariantId",
                table: "tblProductVariantItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariants_Translates_LangId",
                table: "tblProductVariants_Translates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductVariants_Translates_ProductVariantId",
                table: "tblProductVariants_Translates",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSeller_Translates_LangId",
                table: "tblSeller_Translates",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSeller_Translates_LogoId",
                table: "tblSeller_Translates",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSeller_Translates_SellerId",
                table: "tblSeller_Translates",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSellers_UserId",
                table: "tblSellers",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKeywords_Products");

            migrationBuilder.DropTable(
                name: "tblProductAskLikes");

            migrationBuilder.DropTable(
                name: "tblProductMedia");

            migrationBuilder.DropTable(
                name: "tblProductPrices");

            migrationBuilder.DropTable(
                name: "tblProductPropertiesValues");

            migrationBuilder.DropTable(
                name: "tblProductPropertis_Translates");

            migrationBuilder.DropTable(
                name: "tblProductReviewsAttribute_Translate");

            migrationBuilder.DropTable(
                name: "tblProductReviewsAttributeValues");

            migrationBuilder.DropTable(
                name: "tblProductReviewsLikes");

            migrationBuilder.DropTable(
                name: "tblProductReviewsMedia");

            migrationBuilder.DropTable(
                name: "tblProductTopic_Translates");

            migrationBuilder.DropTable(
                name: "tblProductVariantItems");

            migrationBuilder.DropTable(
                name: "tblProductVariants_Translates");

            migrationBuilder.DropTable(
                name: "tblSeller_Translates");

            migrationBuilder.DropTable(
                name: "tblKeywords");

            migrationBuilder.DropTable(
                name: "tblProductAsk");

            migrationBuilder.DropTable(
                name: "tblProductPropertis");

            migrationBuilder.DropTable(
                name: "tblProductReviewsAttribute");

            migrationBuilder.DropTable(
                name: "tblProductReviews");

            migrationBuilder.DropTable(
                name: "tblProductVariants");

            migrationBuilder.DropTable(
                name: "tblSellers");

            migrationBuilder.DropTable(
                name: "tblProductTopic");

            migrationBuilder.DropTable(
                name: "tblProductSellers");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropColumn(
                name: "IsSeller",
                table: "AspNetUsers");
        }
    }
}
