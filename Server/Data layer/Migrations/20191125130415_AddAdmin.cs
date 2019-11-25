using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_layer.Migrations
{
    public partial class AddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Winkelwagens_Klanten_KlantId",
                table: "Winkelwagens");

            migrationBuilder.DropIndex(
                name: "IX_Winkelwagens_KlantId",
                table: "Winkelwagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klanten",
                table: "Klanten");

            migrationBuilder.DropIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropColumn(
                name: "KlantId",
                table: "Winkelwagens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "KlantId",
                table: "Bestellingen");

            migrationBuilder.AddColumn<string>(
                name: "KlantAzureId",
                table: "Winkelwagens",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AzureId",
                table: "Klanten",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KlantAzureId",
                table: "Bestellingen",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klanten",
                table: "Klanten",
                column: "AzureId");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AzureId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AzureId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Winkelwagens_KlantAzureId",
                table: "Winkelwagens",
                column: "KlantAzureId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantAzureId",
                table: "Bestellingen",
                column: "KlantAzureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Klanten_KlantAzureId",
                table: "Bestellingen",
                column: "KlantAzureId",
                principalTable: "Klanten",
                principalColumn: "AzureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Winkelwagens_Klanten_KlantAzureId",
                table: "Winkelwagens",
                column: "KlantAzureId",
                principalTable: "Klanten",
                principalColumn: "AzureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Klanten_KlantAzureId",
                table: "Bestellingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Winkelwagens_Klanten_KlantAzureId",
                table: "Winkelwagens");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Winkelwagens_KlantAzureId",
                table: "Winkelwagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klanten",
                table: "Klanten");

            migrationBuilder.DropIndex(
                name: "IX_Bestellingen_KlantAzureId",
                table: "Bestellingen");

            migrationBuilder.DropColumn(
                name: "KlantAzureId",
                table: "Winkelwagens");

            migrationBuilder.DropColumn(
                name: "KlantAzureId",
                table: "Bestellingen");

            migrationBuilder.AddColumn<int>(
                name: "KlantId",
                table: "Winkelwagens",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AzureId",
                table: "Klanten",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Klanten",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "KlantId",
                table: "Bestellingen",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klanten",
                table: "Klanten",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Winkelwagens_KlantId",
                table: "Winkelwagens",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Winkelwagens_Klanten_KlantId",
                table: "Winkelwagens",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
