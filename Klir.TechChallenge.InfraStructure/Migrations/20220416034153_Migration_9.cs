using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Klir.TechChallenge.InfraStructure.Migrations
{
    public partial class Migration_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdProduct",
                table: "ShoppingCart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "ShoppingCart");
        }
    }
}
