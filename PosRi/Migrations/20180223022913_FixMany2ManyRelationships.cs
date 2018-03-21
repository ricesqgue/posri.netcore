using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PosRi.Migrations
{
    public partial class FixMany2ManyRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Roles_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorBrand_Vendors_BrandId",
                table: "VendorBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorBrand_Brands_VendorId",
                table: "VendorBrand");

            migrationBuilder.CreateTable(
                name: "UserStore",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStore", x => new { x.UserId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_UserStore_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStore_StoreId",
                table: "UserStore",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Roles_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorBrand_Brands_BrandId",
                table: "VendorBrand",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorBrand_Vendors_VendorId",
                table: "VendorBrand",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Roles_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorBrand_Brands_BrandId",
                table: "VendorBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorBrand_Vendors_VendorId",
                table: "VendorBrand");

            migrationBuilder.DropTable(
                name: "UserStore");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Roles_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorBrand_Vendors_BrandId",
                table: "VendorBrand",
                column: "BrandId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorBrand_Brands_VendorId",
                table: "VendorBrand",
                column: "VendorId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
