using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PosRi.Migrations
{
    public partial class AddSubCategoryInProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHeaders_Categories_CategoryId",
                table: "ProductHeaders");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "ProductHeaders",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ProductHeaders",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHeaders_CategoryId",
                table: "ProductHeaders",
                newName: "IX_ProductHeaders_SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHeaders_SubCategories_SubCategoryId",
                table: "ProductHeaders",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHeaders_SubCategories_SubCategoryId",
                table: "ProductHeaders");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "ProductHeaders",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "ProductHeaders",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHeaders_SubCategoryId",
                table: "ProductHeaders",
                newName: "IX_ProductHeaders_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHeaders_Categories_CategoryId",
                table: "ProductHeaders",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
