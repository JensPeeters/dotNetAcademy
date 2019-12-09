using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_layer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AzureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Prijs = table.Column<double>(nullable: false),
                    Categorie = table.Column<string>(nullable: true),
                    FotoURLCard = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Beschrijving = table.Column<string>(maxLength: 120, nullable: true),
                    LangeBeschrijving = table.Column<string>(nullable: true),
                    Titel = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TrajectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_Product_TrajectID",
                        column: x => x.TrajectID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bestellingen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    TotaalPrijs = table.Column<double>(nullable: false),
                    KlantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestellingen_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Winkelwagens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    KlantId = table.Column<int>(nullable: true),
                    TotaalPrijs = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winkelwagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Winkelwagens_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BestellingItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductID = table.Column<int>(nullable: true),
                    Aantal = table.Column<int>(nullable: false),
                    BestellingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestellingItem_Bestellingen_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BestellingItem_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WinkelwagenItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductID = table.Column<int>(nullable: true),
                    Aantal = table.Column<int>(nullable: false),
                    WinkelwagenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinkelwagenItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WinkelwagenItem_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WinkelwagenItem_Winkelwagens_WinkelwagenId",
                        column: x => x.WinkelwagenId,
                        principalTable: "Winkelwagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingItem_BestellingId",
                table: "BestellingItem",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingItem_ProductID",
                table: "BestellingItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Titel",
                table: "Product",
                column: "Titel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_TrajectID",
                table: "Product",
                column: "TrajectID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Titel1",
                table: "Product",
                column: "Titel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WinkelwagenItem_ProductID",
                table: "WinkelwagenItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_WinkelwagenItem_WinkelwagenId",
                table: "WinkelwagenItem",
                column: "WinkelwagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Winkelwagens_KlantId",
                table: "Winkelwagens",
                column: "KlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingItem");

            migrationBuilder.DropTable(
                name: "WinkelwagenItem");

            migrationBuilder.DropTable(
                name: "Bestellingen");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Winkelwagens");

            migrationBuilder.DropTable(
                name: "Klanten");
        }
    }
}
