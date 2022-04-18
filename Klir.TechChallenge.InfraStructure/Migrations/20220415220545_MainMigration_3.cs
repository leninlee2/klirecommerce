using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Klir.TechChallenge.InfraStructure.Migrations
{
    public partial class MainMigration_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ShoppingCart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Promotion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));


            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Promotion",
            //    table: "Promotion",
            //    column: "Id");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "Id",
            //    table: "Product",
            //    type: "uniqueidentifier",
            //    nullable: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Product",
            //    table: "Product",
            //    column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product");
        }
    }
}
