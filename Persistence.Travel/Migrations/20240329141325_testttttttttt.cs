using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Travel.Migrations
{
    /// <inheritdoc />
    public partial class testttttttttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AirQuality",
                table: "Housings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AirDescription",
                table: "Housings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Owners_OwnerId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_Housings_OwnerId",
                table: "Housings");

            migrationBuilder.AlterColumn<int>(
                name: "AirQuality",
                table: "Housings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AirDescription",
                table: "Housings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
