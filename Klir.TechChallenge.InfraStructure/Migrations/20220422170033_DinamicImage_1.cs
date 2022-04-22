using Microsoft.EntityFrameworkCore.Migrations;

namespace Klir.TechChallenge.InfraStructure.Migrations
{
    public partial class DinamicImage_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Product");
        }
    }
}
