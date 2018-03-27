﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PosRi.Entities.Context;
using System;

namespace PosRi.Migrations
{
    [DbContext(typeof(PosRiContext))]
    [Migration("20180327033113_VendorAndClientAddressFieldFix")]
    partial class VendorAndClientAddressFieldFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PosRi.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("PosRi.Entities.CashFound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CashRegisterId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CashRegisterId");

                    b.HasIndex("UserId");

                    b.ToTable("CashFounds");
                });

            modelBuilder.Entity("PosRi.Entities.CashRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("CashRegisters");
                });

            modelBuilder.Entity("PosRi.Entities.CashRegisterMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action")
                        .IsRequired();

                    b.Property<int>("CashRegisterId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CashRegisterId");

                    b.HasIndex("UserId");

                    b.ToTable("CashRegisterMoves");
                });

            modelBuilder.Entity("PosRi.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PosRi.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<string>("Rfc")
                        .HasMaxLength(25);

                    b.Property<int>("StateId");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("PosRi.Entities.ClientDebt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<decimal>("Debt");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("SaleHeaderId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SaleHeaderId");

                    b.ToTable("ClientDebts");
                });

            modelBuilder.Entity("PosRi.Entities.ClientPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Card");

                    b.Property<decimal>("Cash");

                    b.Property<int>("ClientDebtId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Total");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ClientDebtId");

                    b.HasIndex("UserId");

                    b.ToTable("ClientPayments");
                });

            modelBuilder.Entity("PosRi.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("RgbHex")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("PosRi.Entities.InventoryProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastAdd");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.ToTable("InventoryProducts");
                });

            modelBuilder.Entity("PosRi.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("ColorPrimaryId");

                    b.Property<int>("ColorSecondaryId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("ExtraDescription")
                        .HasMaxLength(150);

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductHeaderId");

                    b.Property<decimal>("PurchasePrice");

                    b.Property<int>("SizeId");

                    b.Property<decimal>("SpecialPrice");

                    b.HasKey("Id");

                    b.HasIndex("ColorPrimaryId");

                    b.HasIndex("ColorSecondaryId");

                    b.HasIndex("ProductHeaderId");

                    b.HasIndex("SizeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PosRi.Entities.ProductHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrandId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductHeaders");
                });

            modelBuilder.Entity("PosRi.Entities.PurchaseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductId");

                    b.Property<int>("PurchaseHeaderId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("SalePrice");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseHeaderId");

                    b.ToTable("PurchaseDetails");
                });

            modelBuilder.Entity("PosRi.Entities.PurchaseHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Discount");

                    b.Property<decimal>("PaidCard");

                    b.Property<decimal>("PaidCash");

                    b.Property<decimal>("PaidCredit");

                    b.Property<DateTime>("PurchaseDate");

                    b.Property<decimal>("SubTotal");

                    b.Property<decimal>("Total");

                    b.Property<int>("UserId");

                    b.Property<int>("VendorId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("PurchaseHeaders");
                });

            modelBuilder.Entity("PosRi.Entities.Role", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PosRi.Entities.SaleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int>("SaleHeaderId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleHeaderId");

                    b.ToTable("SaleDetails");
                });

            modelBuilder.Entity("PosRi.Entities.SaleHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CashRegisterId");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Discount");

                    b.Property<decimal>("PaidCard");

                    b.Property<decimal>("PaidCash");

                    b.Property<decimal>("PaidCredit");

                    b.Property<decimal>("SubTotal");

                    b.Property<decimal>("Total");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CashRegisterId");

                    b.HasIndex("ClientId");

                    b.HasIndex("UserId");

                    b.ToTable("SaleHeaders");
                });

            modelBuilder.Entity("PosRi.Entities.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("PosRi.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("PosRi.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("PosRi.Entities.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("PosRi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime>("HireDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PosRi.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("PosRi.Entities.UserStore", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("StoreId");

                    b.HasKey("UserId", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("UserStore");
                });

            modelBuilder.Entity("PosRi.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<string>("Rfc")
                        .HasMaxLength(25);

                    b.Property<int>("StateId");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("PosRi.Entities.VendorBrand", b =>
                {
                    b.Property<int>("VendorId");

                    b.Property<int>("BrandId");

                    b.HasKey("VendorId", "BrandId");

                    b.HasIndex("BrandId");

                    b.ToTable("VendorBrand");
                });

            modelBuilder.Entity("PosRi.Entities.VendorDebt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<DateTime>("CreateDate");

                    b.Property<decimal>("Debt");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("PurchaseHeaderId");

                    b.Property<int>("VendorId");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseHeaderId");

                    b.HasIndex("VendorId");

                    b.ToTable("VendorDebts");
                });

            modelBuilder.Entity("PosRi.Entities.VendorPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Card");

                    b.Property<decimal>("Cash");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Total");

                    b.Property<int>("UserId");

                    b.Property<int>("VendorDebtId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorDebtId");

                    b.ToTable("VendorPayments");
                });

            modelBuilder.Entity("PosRi.Entities.CashFound", b =>
                {
                    b.HasOne("PosRi.Entities.CashRegister", "CashRegister")
                        .WithMany()
                        .HasForeignKey("CashRegisterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.CashRegister", b =>
                {
                    b.HasOne("PosRi.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.CashRegisterMove", b =>
                {
                    b.HasOne("PosRi.Entities.CashRegister", "CashRegister")
                        .WithMany()
                        .HasForeignKey("CashRegisterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.Client", b =>
                {
                    b.HasOne("PosRi.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.ClientDebt", b =>
                {
                    b.HasOne("PosRi.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.SaleHeader", "SaleHeader")
                        .WithMany()
                        .HasForeignKey("SaleHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.ClientPayment", b =>
                {
                    b.HasOne("PosRi.Entities.ClientDebt", "ClientDebt")
                        .WithMany("ClientPayments")
                        .HasForeignKey("ClientDebtId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.InventoryProduct", b =>
                {
                    b.HasOne("PosRi.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.Product", b =>
                {
                    b.HasOne("PosRi.Entities.Color", "ColorPrimary")
                        .WithMany()
                        .HasForeignKey("ColorPrimaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Color", "ColorSecondary")
                        .WithMany()
                        .HasForeignKey("ColorSecondaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.ProductHeader", "ProductHeader")
                        .WithMany("Products")
                        .HasForeignKey("ProductHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.ProductHeader", b =>
                {
                    b.HasOne("PosRi.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.PurchaseDetail", b =>
                {
                    b.HasOne("PosRi.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.PurchaseHeader", "PurchaseHeader")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.PurchaseHeader", b =>
                {
                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.SaleDetail", b =>
                {
                    b.HasOne("PosRi.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.SaleHeader", "SaleHeader")
                        .WithMany("SaleDetails")
                        .HasForeignKey("SaleHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.SaleHeader", b =>
                {
                    b.HasOne("PosRi.Entities.CashRegister", "CashRegister")
                        .WithMany()
                        .HasForeignKey("CashRegisterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.SubCategory", b =>
                {
                    b.HasOne("PosRi.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.UserRole", b =>
                {
                    b.HasOne("PosRi.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.UserStore", b =>
                {
                    b.HasOne("PosRi.Entities.Store", "Store")
                        .WithMany("Users")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany("Stores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.Vendor", b =>
                {
                    b.HasOne("PosRi.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.VendorBrand", b =>
                {
                    b.HasOne("PosRi.Entities.Brand", "Brand")
                        .WithMany("Vendors")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Vendor", "Vendor")
                        .WithMany("Brands")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.VendorDebt", b =>
                {
                    b.HasOne("PosRi.Entities.PurchaseHeader", "PurchaseHeader")
                        .WithMany()
                        .HasForeignKey("PurchaseHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PosRi.Entities.VendorPayment", b =>
                {
                    b.HasOne("PosRi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PosRi.Entities.VendorDebt", "VendorDebt")
                        .WithMany("VendorPayments")
                        .HasForeignKey("VendorDebtId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
