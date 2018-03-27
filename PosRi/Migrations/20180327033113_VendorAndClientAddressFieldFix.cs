using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PosRi.Migrations
{
    public partial class VendorAndClientAddressFieldFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Products_Colors_ColorId",
            //    table: "Products");

            //migrationBuilder.RenameColumn(
            //    name: "ColorId",
            //    table: "Products",
            //    newName: "ColorSecondaryId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Products_ColorId",
            //    table: "Products",
            //    newName: "IX_Products_ColorSecondaryId");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Vendors",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ColorPrimaryId",
            //    table: "Products",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Clients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_ColorPrimaryId",
            //    table: "Products",
            //    column: "ColorPrimaryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Colors_ColorPrimaryId",
            //    table: "Products",
            //    column: "ColorPrimaryId",
            //    principalTable: "Colors",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Colors_ColorSecondaryId",
            //    table: "Products",
            //    column: "ColorSecondaryId",
            //    principalTable: "Colors",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Products_Colors_ColorPrimaryId",
            //    table: "Products");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Products_Colors_ColorSecondaryId",
            //    table: "Products");

            //migrationBuilder.DropIndex(
            //    name: "IX_Products_ColorPrimaryId",
            //    table: "Products");

            //migrationBuilder.DropColumn(
            //    name: "ColorPrimaryId",
            //    table: "Products");

            //migrationBuilder.RenameColumn(
            //    name: "ColorSecondaryId",
            //    table: "Products",
            //    newName: "ColorId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Products_ColorSecondaryId",
            //    table: "Products",
            //    newName: "IX_Products_ColorId");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Vendors",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Colors_ColorId",
            //    table: "Products",
            //    column: "ColorId",
            //    principalTable: "Colors",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
