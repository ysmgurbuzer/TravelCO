﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Travel.Migrations
{
    /// <inheritdoc />
    public partial class Azuredb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PreferredCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LikesArtPlaces = table.Column<bool>(type: "bit", nullable: false),
                    LikesSports = table.Column<bool>(type: "bit", nullable: false),
                    LikesCultureAndHistory = table.Column<bool>(type: "bit", nullable: false),
                    LikesNaturePlaces = table.Column<bool>(type: "bit", nullable: false),
                    LikesFarEastCuisine = table.Column<bool>(type: "bit", nullable: false),
                    LikesFastFood = table.Column<bool>(type: "bit", nullable: false),
                    LikesTraditionalCuisine = table.Column<bool>(type: "bit", nullable: false),
                    LikesNightlife = table.Column<bool>(type: "bit", nullable: false),
                    LikesShopping = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    WalletAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ResponseTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Housings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<int>(type: "int", nullable: false),
                    ImagePathOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePathTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePathThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    BedNumber = table.Column<int>(type: "int", nullable: false),
                    BathNumber = table.Column<int>(type: "int", nullable: false),
                    MaxAccommodates = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Housings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Housings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Housings_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeOwnerId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    HomeOwnerServicesType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerFeatures_Owners_HomeOwnerId",
                        column: x => x.HomeOwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HomeownerId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true),
                    UserId2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerReviews_Owners_HomeownerId",
                        column: x => x.HomeownerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OwnerReviews_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HousingId = table.Column<int>(type: "int", nullable: false),
                    HousingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Housings_HousingId",
                        column: x => x.HousingId,
                        principalTable: "Housings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Housings_HousingId1",
                        column: x => x.HousingId1,
                        principalTable: "Housings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HousingDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HousingDescriptions_Housings_HousingId",
                        column: x => x.HousingId,
                        principalTable: "Housings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HousingFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HousingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HousingFeatures_Housings_HousingId",
                        column: x => x.HousingId,
                        principalTable: "Housings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HousingReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HousingId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HousingReviews_Housings_HousingId",
                        column: x => x.HousingId,
                        principalTable: "Housings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HousingReviews_Housings_HousingId1",
                        column: x => x.HousingId1,
                        principalTable: "Housings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HousingReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HousingId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Housings_HousingId",
                        column: x => x.HousingId,
                        principalTable: "Housings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Housings_HousingId1",
                        column: x => x.HousingId1,
                        principalTable: "Housings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_HousingId",
                table: "Favorites",
                column: "HousingId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_HousingId1",
                table: "Favorites",
                column: "HousingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingDescriptions_HousingId",
                table: "HousingDescriptions",
                column: "HousingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HousingFeatures_HousingId",
                table: "HousingFeatures",
                column: "HousingId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingReviews_HousingId",
                table: "HousingReviews",
                column: "HousingId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingReviews_HousingId1",
                table: "HousingReviews",
                column: "HousingId1");

            migrationBuilder.CreateIndex(
                name: "IX_HousingReviews_UserId",
                table: "HousingReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_LocationId",
                table: "Housings",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Housings_OwnerId",
                table: "Housings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerFeatures_HomeOwnerId",
                table: "OwnerFeatures",
                column: "HomeOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerReviews_HomeownerId",
                table: "OwnerReviews",
                column: "HomeownerId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerReviews_UserId",
                table: "OwnerReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerReviews_UserId1",
                table: "OwnerReviews",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HousingId",
                table: "Reservations",
                column: "HousingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HousingId1",
                table: "Reservations",
                column: "HousingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RolesId",
                table: "UserRoles",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId1",
                table: "UserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "HousingDescriptions");

            migrationBuilder.DropTable(
                name: "HousingFeatures");

            migrationBuilder.DropTable(
                name: "HousingReviews");

            migrationBuilder.DropTable(
                name: "OwnerFeatures");

            migrationBuilder.DropTable(
                name: "OwnerReviews");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Housings");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
