﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversalStationary.Models;

#nullable disable

namespace UniversalStationary.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20250102165629_banners")]
    partial class banners
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UniversalStationary.Models.AddProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("FeaturedProduct")
                        .HasColumnType("bit");

                    b.Property<bool?>("NewArrival")
                        .HasColumnType("bit");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stock")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Trendingproducts")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productpicture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("addproduct");
                });

            modelBuilder.Entity("UniversalStationary.Models.LogoChangeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Phonenumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("logofile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sitebanners")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sitediscription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("logochanges");
                });

            modelBuilder.Entity("UniversalStationary.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("589a5f94-fa13-4d88-b261-7acdd0305ebd"),
                            Address = "Malir Karachi",
                            City = "Karachi",
                            Created = new DateTime(2025, 1, 2, 21, 56, 28, 761, DateTimeKind.Local).AddTicks(2908),
                            Email = "superadmin@gmail.com",
                            Gender = "Male",
                            Password = "$2a$11$DmpAD2HB8yOn8J9UPZ5Ot.43fyaMnqWMgr.cHUJbW6/AxWSl2Q9i2",
                            PhoneNumber = "1234567890",
                            PostalCode = "12345",
                            ProfilePicture = "AllImages/userimage.png",
                            Role = "Admin",
                            Updated = new DateTime(2025, 1, 2, 21, 56, 28, 761, DateTimeKind.Local).AddTicks(2939),
                            UserName = "SuperAdmin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
