﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wahama;

namespace Wahama.Migrations
{
    [DbContext(typeof(WarhammerContext))]
    [Migration("20190923212730_aosd")]
    partial class aosd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wahama.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(N'Россия')")
                        .HasMaxLength(50);

                    b.Property<string>("Flat")
                        .HasMaxLength(10);

                    b.Property<string>("Floor");

                    b.Property<string>("Station")
                        .HasMaxLength(50);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Wahama.Check", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<bool>("IsPaid");

                    b.Property<int>("PaymentMethodId");

                    b.Property<int?>("SellerId");

                    b.Property<int>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("SellerId");

                    b.ToTable("Check");
                });

            modelBuilder.Entity("Wahama.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Wahama.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new { Id = 1, Name = "admin" },
                        new { Id = 2, Name = "user" },
                        new { Id = 3, Name = "moderator" },
                        new { Id = 4, Name = "employee" }
                    );
                });

            modelBuilder.Entity("Wahama.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<int?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Wahama.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckId");

                    b.Property<int?>("ShoppingCartId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CheckId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Wahama.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("Wahama.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImageSource");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("Price");

                    b.Property<int>("ProductFractionId");

                    b.Property<int>("ProductTypeId");

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");

                    b.HasIndex("ProductFractionId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Wahama.ProductAlliance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageSource");

                    b.Property<int>("ProductSettingId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ProductSettingId");

                    b.ToTable("ProductAlliance");
                });

            modelBuilder.Entity("Wahama.ProductFraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageSource");

                    b.Property<int>("ProductAllianceId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ProductAllianceId");

                    b.ToTable("ProductFraction");
                });

            modelBuilder.Entity("Wahama.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageSource")
                        .IsRequired();

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("Wahama.ProductSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageSource");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ProductSetting");
                });

            modelBuilder.Entity("Wahama.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("Wahama.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Post")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("Wahama.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("Wahama.WarehouseList", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("WarehouseList");
                });

            modelBuilder.Entity("Wahama.WarehouseProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAvailable");

                    b.Property<int>("ProductId");

                    b.Property<int>("PurchasePrice");

                    b.Property<int>("Quantity");

                    b.Property<int>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseProducts");
                });

            modelBuilder.Entity("Wahama.Check", b =>
                {
                    b.HasOne("Wahama.Customer", "Customer")
                        .WithMany("Check")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Check_Customer");

                    b.HasOne("Wahama.PaymentMethod", "PaymentMethod")
                        .WithMany("Check")
                        .HasForeignKey("PaymentMethodId")
                        .HasConstraintName("FK_Check_PaymentMethod");

                    b.HasOne("Wahama.Seller")
                        .WithMany("Check")
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("Wahama.Customer", b =>
                {
                    b.HasOne("Wahama.Address", "Address")
                        .WithMany("Customer")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_Customer_Address");
                });

            modelBuilder.Entity("Wahama.Models.User", b =>
                {
                    b.HasOne("Wahama.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Wahama.Order", b =>
                {
                    b.HasOne("Wahama.Check", "Check")
                        .WithMany("Order")
                        .HasForeignKey("CheckId")
                        .HasConstraintName("FK_Order_Check");

                    b.HasOne("Wahama.ShoppingCart")
                        .WithMany("Order")
                        .HasForeignKey("ShoppingCartId");
                });

            modelBuilder.Entity("Wahama.Product", b =>
                {
                    b.HasOne("Wahama.ProductFraction", "ProductFraction")
                        .WithMany("Product")
                        .HasForeignKey("ProductFractionId")
                        .HasConstraintName("FK_Product_ProductFraction");

                    b.HasOne("Wahama.ProductType", "ProductType")
                        .WithMany("Product")
                        .HasForeignKey("ProductTypeId")
                        .HasConstraintName("FK_Product_ProductType1");
                });

            modelBuilder.Entity("Wahama.ProductAlliance", b =>
                {
                    b.HasOne("Wahama.ProductSetting", "ProductSetting")
                        .WithMany("ProductAlliance")
                        .HasForeignKey("ProductSettingId")
                        .HasConstraintName("FK_ProductAlliance_ProductSetting");
                });

            modelBuilder.Entity("Wahama.ProductFraction", b =>
                {
                    b.HasOne("Wahama.ProductAlliance", "ProductAlliance")
                        .WithMany("ProductFraction")
                        .HasForeignKey("ProductAllianceId")
                        .HasConstraintName("FK_ProductFraction_ProductAlliance");
                });

            modelBuilder.Entity("Wahama.ProductImage", b =>
                {
                    b.HasOne("Wahama.Product", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductImage_Product");
                });

            modelBuilder.Entity("Wahama.ShoppingCart", b =>
                {
                    b.HasOne("Wahama.Product", "Product")
                        .WithMany("ShoppingCart")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ShoppingCart_Product");
                });

            modelBuilder.Entity("Wahama.WarehouseProducts", b =>
                {
                    b.HasOne("Wahama.Product", "Product")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Warehouse_Product");

                    b.HasOne("Wahama.WarehouseList", "Warehouse")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("FK_WarehouseProducts_WarehouseList");
                });
#pragma warning restore 612, 618
        }
    }
}
