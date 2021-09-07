using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrancaBeauty.Infrastructure.EFCore.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetRoles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblAccessLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccessLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFileServers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpDomin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    FtpData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFileServers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAccessLevel_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    AccessLevelId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccessLevel_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAccessLevel_Roles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAccessLevel_Roles_tblAccessLevels_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "tblAccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "tblAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProviceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Plaque = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileServerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SizeOnDisk = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFiles_tblFileServers_FileServerId",
                        column: x => x.FileServerId,
                        principalTable: "tblFileServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCategoris",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_tblCategoris_tblFiles_ImageId",
                        column: x => x.ImageId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FlagImgId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCountries_tblFiles_FlagImgId",
                        column: x => x.FlagImgId,
                        principalTable: "tblFiles",
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
                name: "tblLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NativeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsRtl = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UseForSiteLanguage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblLanguages_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    ProfileImgId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    AccessLevelId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordPhoneNumber = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    LastTrySentSms = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSeller = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tblAccessLevels_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "tblAccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tblFiles_ProfileImgId",
                        column: x => x.ProfileImgId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
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
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategory_Translates_tblCategoris_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCategory_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCountries_Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountries_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCountries_Translates_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblCountries_Translates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "tblSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SiteTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SiteEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SitePhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsInManufacture = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSettings_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTamplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTamplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTamplates_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
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
                name: "tblProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    AuthorUserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UniqueNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_tblProducts_tblLanguages_LangId",
                        column: x => x.LangId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_tblProductPrices_tblCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "tblCurrencies",
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
                name: "tblProductSellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    SellerUserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Guarantee = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SendFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ParentId",
                table: "AspNetRoles",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccessLevelId",
                table: "AspNetUsers",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LangId",
                table: "AspNetUsers",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImgId",
                table: "AspNetUsers",
                column: "ProfileImgId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblAccessLevel_Roles_AccessLevelId",
                table: "tblAccessLevel_Roles",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAccessLevel_Roles_RoleId",
                table: "tblAccessLevel_Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_CityId",
                table: "tblAddress",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_CountryId",
                table: "tblAddress",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_ProviceId",
                table: "tblAddress",
                column: "ProviceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_UserId",
                table: "tblAddress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoris_ImageId",
                table: "tblCategoris",
                column: "ImageId");

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
                name: "IX_tblCountries_FlagImgId",
                table: "tblCountries",
                column: "FlagImgId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCountries_Translates_CountryId",
                table: "tblCountries_Translates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCountries_Translates_LangId",
                table: "tblCountries_Translates",
                column: "LangId");

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

            migrationBuilder.CreateIndex(
                name: "IX_tblFiles_FileServerId",
                table: "tblFiles",
                column: "FileServerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiles_UserId",
                table: "tblFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKeywords_Products_KeywordId",
                table: "tblKeywords_Products",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKeywords_Products_ProductId",
                table: "tblKeywords_Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLanguages_CountryId",
                table: "tblLanguages",
                column: "CountryId");

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
                name: "IX_tblProductPrices_CurrencyId",
                table: "tblProductPrices",
                column: "CurrencyId");

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
                name: "IX_tblProducts_LangId",
                table: "tblProducts",
                column: "LangId");

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

            migrationBuilder.CreateIndex(
                name: "IX_tblSettings_LangId",
                table: "tblSettings",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTamplates_LangId",
                table: "tblTamplates",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAddress_AspNetUsers_UserId",
                table: "tblAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAddress_tblCities_CityId",
                table: "tblAddress",
                column: "CityId",
                principalTable: "tblCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAddress_tblCountries_CountryId",
                table: "tblAddress",
                column: "CountryId",
                principalTable: "tblCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAddress_tblProvinces_ProviceId",
                table: "tblAddress",
                column: "ProviceId",
                principalTable: "tblProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblFiles_AspNetUsers_UserId",
                table: "tblFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFiles_AspNetUsers_UserId",
                table: "tblFiles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tblAccessLevel_Roles");

            migrationBuilder.DropTable(
                name: "tblAddress");

            migrationBuilder.DropTable(
                name: "tblCategory_Translates");

            migrationBuilder.DropTable(
                name: "tblCities_Translates");

            migrationBuilder.DropTable(
                name: "tblCountries_Translates");

            migrationBuilder.DropTable(
                name: "tblCurrency_Translates");

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
                name: "tblProvinces_Translate");

            migrationBuilder.DropTable(
                name: "tblSeller_Translates");

            migrationBuilder.DropTable(
                name: "tblSettings");

            migrationBuilder.DropTable(
                name: "tblTamplates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "tblCities");

            migrationBuilder.DropTable(
                name: "tblKeywords");

            migrationBuilder.DropTable(
                name: "tblProductAsk");

            migrationBuilder.DropTable(
                name: "tblCurrencies");

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
                name: "tblProvinces");

            migrationBuilder.DropTable(
                name: "tblProductTopic");

            migrationBuilder.DropTable(
                name: "tblProductSellers");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblCategoris");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblAccessLevels");

            migrationBuilder.DropTable(
                name: "tblLanguages");

            migrationBuilder.DropTable(
                name: "tblCountries");

            migrationBuilder.DropTable(
                name: "tblFiles");

            migrationBuilder.DropTable(
                name: "tblFileServers");
        }
    }
}
