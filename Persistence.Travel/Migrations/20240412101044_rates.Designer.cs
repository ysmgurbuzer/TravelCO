﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Travel.Context;

#nullable disable

namespace Persistence.Travel.Migrations
{
    [DbContext(typeof(TravelContext))]
    [Migration("20240412101044_rates")]
    partial class rates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Travel.Entities.AIRecommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("HomeLatitude")
                        .HasColumnType("float");

                    b.Property<double>("HomeLongitude")
                        .HasColumnType("float");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("PreferredCategories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SurveyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SurveyId");

                    b.HasIndex("UserId");

                    b.ToTable("AI");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Favorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HousingId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HousingId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenderType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Housing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AirDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AirQuality")
                        .HasColumnType("int");

                    b.Property<int>("BathNumber")
                        .HasColumnType("int");

                    b.Property<int>("BedNumber")
                        .HasColumnType("int");

                    b.Property<int>("CategoryName")
                        .HasColumnType("int");

                    b.Property<string>("FloorLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathOne")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("MaxAccommodates")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Housings");
                });

            modelBuilder.Entity("Domain.Travel.Entities.HousingDescriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HousingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HousingId")
                        .IsUnique();

                    b.ToTable("HousingDescriptions");
                });

            modelBuilder.Entity("Domain.Travel.Entities.HousingFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("HousingId")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HousingId");

                    b.ToTable("HousingFeatures");
                });

            modelBuilder.Entity("Domain.Travel.Entities.HousingReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HousingId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HousingId");

                    b.HasIndex("UserId");

                    b.ToTable("HousingReviews");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("latitude")
                        .HasColumnType("float");

                    b.Property<double>("longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ResponseTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Title")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Domain.Travel.Entities.OwnerFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("HomeOwnerId")
                        .HasColumnType("int");

                    b.Property<int>("HomeOwnerServicesType")
                        .HasColumnType("int");

                    b.Property<int>("ServicesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HomeOwnerId");

                    b.ToTable("OwnerFeatures");
                });

            modelBuilder.Entity("Domain.Travel.Entities.OwnerReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HomeownerId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("OwnerReviews");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AIRecommendationId")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<string>("Types")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AIRecommendationId");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HousingId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfAdults")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfChildren")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HousingId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("LikesArtPlaces")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesCultureAndHistory")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesFarEastCuisine")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesFastFood")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesNaturePlaces")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesNightlife")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesShopping")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesSports")
                        .HasColumnType("bit");

                    b.Property<bool>("LikesTraditionalCuisine")
                        .HasColumnType("bit");

                    b.Property<string>("PreferredCategories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Domain.Travel.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("WalletAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Travel.Entities.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Domain.Travel.Entities.AIRecommendation", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Domain.Travel.Entities.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId");

                    b.HasOne("Domain.Travel.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Favorites", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Housing", "FavoriteHousings")
                        .WithMany("Favorites")
                        .HasForeignKey("HousingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Travel.Entities.User", "Users")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FavoriteHousings");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Housing", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Travel.Entities.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Travel.Entities.HousingDescriptions", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Housing", "Housing")
                        .WithOne("HousingDescriptions")
                        .HasForeignKey("Domain.Travel.Entities.HousingDescriptions", "HousingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Housing");
                });

            modelBuilder.Entity("Domain.Travel.Entities.HousingFeatures", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Housing", "Housing")
                        .WithMany("HousingFeatures")
                        .HasForeignKey("HousingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Housing");
                });

            modelBuilder.Entity("Domain.Travel.Entities.HousingReview", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Housing", "Housing")
                        .WithMany("HousingReviews")
                        .HasForeignKey("HousingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Travel.Entities.User", "User")
                        .WithMany("HousingReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Housing");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Owner", b =>
                {
                    b.HasOne("Domain.Travel.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Travel.Entities.OwnerFeatures", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Owner", "Homeowner")
                        .WithMany("OwnerFeatures")
                        .HasForeignKey("HomeOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Homeowner");
                });

            modelBuilder.Entity("Domain.Travel.Entities.OwnerReview", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Owner", "Owner")
                        .WithMany("OwnerReviews")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Travel.Entities.User", "User")
                        .WithMany("OwnerReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Place", b =>
                {
                    b.HasOne("Domain.Travel.Entities.AIRecommendation", null)
                        .WithMany("Places")
                        .HasForeignKey("AIRecommendationId");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Reservation", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Housing", "Housing")
                        .WithMany("Reservations")
                        .HasForeignKey("HousingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Travel.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Housing");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Travel.Entities.User", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("Domain.Travel.Entities.UserRoles", b =>
                {
                    b.HasOne("Domain.Travel.Entities.Roles", "Role")
                        .WithMany("userRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Travel.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Travel.Entities.AIRecommendation", b =>
                {
                    b.Navigation("Places");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Housing", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("HousingDescriptions")
                        .IsRequired();

                    b.Navigation("HousingFeatures");

                    b.Navigation("HousingReviews");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Owner", b =>
                {
                    b.Navigation("OwnerFeatures");

                    b.Navigation("OwnerReviews");
                });

            modelBuilder.Entity("Domain.Travel.Entities.Roles", b =>
                {
                    b.Navigation("userRoles");
                });

            modelBuilder.Entity("Domain.Travel.Entities.User", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("HousingReviews");

                    b.Navigation("OwnerReviews");

                    b.Navigation("Reservations");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
