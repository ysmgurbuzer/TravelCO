using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Travel.Migrations
{
    /// <inheritdoc />
    public partial class forOwnerHouseFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Owners_OwnerId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_Housings_LocationId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_Housings_OwnerId",
                table: "Housings");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_LocationId",
                table: "Housings",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Housings_LocationId",
                table: "Housings");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_LocationId",
                table: "Housings",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Housings_OwnerId",
                table: "Housings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Owners_OwnerId",
                table: "Housings",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
