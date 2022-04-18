using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Klir.TechChallenge.InfraStructure.Migrations
{
    public partial class MainMigration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn("Id", "ShoppingCart");

            //migrationBuilder.DropColumn("Id", "Promotion");

            //migrationBuilder.DropColumn("Id", "Product");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "Id",
            //    table: "ShoppingCart",
            //    type: "uniqueidentifier",
            //    nullable: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_ShoppingCart",
            //    table: "ShoppingCart",
            //    column: "Id");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "Id",
            //    table: "Promotion",
            //    type: "uniqueidentifier",
            //    nullable: false);

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
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Promotion",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
