using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rusu_Denisa_Proiect.Data.Migrations
{
    public partial class modified_bouquet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Bouquets",
                type: "real",
                nullable: true,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bouquets");
        }
    }
}
