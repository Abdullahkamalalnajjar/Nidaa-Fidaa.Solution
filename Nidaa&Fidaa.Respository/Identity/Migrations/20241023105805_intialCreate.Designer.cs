﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nidaa_Fidaa.Repository.Identity;

#nullable disable

namespace Nidaa_Fidaa.Respository.Identity.Migrations
{
    [DbContext(typeof(AppIdentityDbContext))]
    [Migration("20241023105805_intialCreate")]
    partial class intialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.ToTable("Basket");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductSizeId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductSizeId")
                        .IsUnique();

                    b.ToTable("BasketItem");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("BasePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DeliveryTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DiscountedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ShopCategoryShopId")
                        .HasColumnType("int");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopCategoryShopId");

                    b.HasIndex("ShopId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductAddition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BasketItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketItemId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAddition");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductFavourite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerIdentityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerIdentityId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFavourite");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSize");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BaseShopPhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DeliveryTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TraderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ShopCategory", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ShopId1")
                        .HasColumnType("int");

                    b.HasKey("ShopId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopId1");

                    b.ToTable("ShopCategory");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ShopFavourite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerIdentityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerIdentityId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopFavourite");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Identity.CustomerIdentity", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zone")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("CustomerIdentity");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Identity.DriverIdentity", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<string>("DriverLicensePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FrontViewPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullViewWithPlatePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DriverIdentity_Governorate");

                    b.Property<string>("IDCardPhotoBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDCardPhotoFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDNumber")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlateNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Municipality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DriverIdentity_Name");

                    b.Property<string>("RearViewPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransportationType")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("DriverIdentity");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Identity.TraderIdentity", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TraderIdentity_Address");

                    b.Property<string>("CommercialRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TraderIdentity_Governorate");

                    b.Property<string>("Municipality")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TraderIdentity_Municipality");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TraderIdentity_Name");

                    b.Property<string>("TradeActivityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TraderId")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("TraderIdentity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Basket", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Customer", "Customer")
                        .WithOne("Basket")
                        .HasForeignKey("Nidaa_Fidaa.Core.Entities.Basket", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.BasketItem", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Basket", "Basket")
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.ProductSize", "ProductSize")
                        .WithOne("BasketItem")
                        .HasForeignKey("Nidaa_Fidaa.Core.Entities.BasketItem", "ProductSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Product");

                    b.Navigation("ProductSize");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Image", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Order", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.OrderItem", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Product", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.ShopCategory", null)
                        .WithMany("Products")
                        .HasForeignKey("ShopCategoryShopId");

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Shop", "Shop")
                        .WithMany("Products")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductAddition", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.BasketItem", null)
                        .WithMany("Additions")
                        .HasForeignKey("BasketItemId");

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Product", "Product")
                        .WithMany("ProductAdditions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductFavourite", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Customer", "Customer")
                        .WithMany("ProductFavourites")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Identity.CustomerIdentity", null)
                        .WithMany("ProductFavourites")
                        .HasForeignKey("CustomerIdentityId");

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Product", "Product")
                        .WithMany("ProductFavourites")
                        .HasForeignKey("ProductId");

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductSize", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Product", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ShopCategory", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Category", "Category")
                        .WithMany("ShopCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Shop", "Shop")
                        .WithMany("ShopCategory")
                        .HasForeignKey("ShopId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ShopFavourite", b =>
                {
                    b.HasOne("Nidaa_Fidaa.Core.Entities.Customer", "Customer")
                        .WithMany("ShopFavourites")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Identity.CustomerIdentity", null)
                        .WithMany("ShopFavourites")
                        .HasForeignKey("CustomerIdentityId");

                    b.HasOne("Nidaa_Fidaa.Core.Entities.Shop", "Shop")
                        .WithMany("ShopFavourites")
                        .HasForeignKey("ShopId");

                    b.Navigation("Customer");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Basket", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.BasketItem", b =>
                {
                    b.Navigation("Additions");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("ShopCategory");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Customer", b =>
                {
                    b.Navigation("Basket");

                    b.Navigation("ProductFavourites");

                    b.Navigation("ShopFavourites");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("OrderItems");

                    b.Navigation("ProductAdditions");

                    b.Navigation("ProductFavourites");

                    b.Navigation("ProductSizes");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ProductSize", b =>
                {
                    b.Navigation("BasketItem")
                        .IsRequired();
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Shop", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("ShopCategory");

                    b.Navigation("ShopFavourites");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.ShopCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Nidaa_Fidaa.Core.Entities.Identity.CustomerIdentity", b =>
                {
                    b.Navigation("ProductFavourites");

                    b.Navigation("ShopFavourites");
                });
#pragma warning restore 612, 618
        }
    }
}
