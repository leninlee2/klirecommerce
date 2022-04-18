using Microsoft.EntityFrameworkCore.Migrations;

namespace Klir.TechChallenge.InfraStructure.Migrations
{
    public partial class NewFeatures_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Close",
                table: "GroupShoopingCart",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Close",
                table: "GroupShoopingCart");
        }
    }
}
