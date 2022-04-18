using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Klir.TechChallenge.InfraStructure.Migrations
{
    public partial class Migration_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<Guid>(
            //    name: "IdPromotion",
            //    table: "ShoppingCart",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "IdPromotion",
            //    table: "Product",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_ShoppingCart",
            //    table: "ShoppingCart",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Promotion",
            //    table: "Promotion",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Product",
            //    table: "Product",
            //    column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "IdPromotion",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "IdPromotion",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
