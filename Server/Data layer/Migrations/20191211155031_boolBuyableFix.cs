using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_layer.Migrations
{
    public partial class boolBuyableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBuyable",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBuyable",
                table: "Product");
        }
    }
}
