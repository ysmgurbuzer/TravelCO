using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Travel.Migrations
{
    /// <inheritdoc />
    public partial class routes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source_Lat = table.Column<double>(type: "float", nullable: false),
                    Source_Long = table.Column<double>(type: "float", nullable: false),
                    Target_Lat = table.Column<double>(type: "float", nullable: false),
                    Target_Long = table.Column<double>(type: "float", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<double>(type: "float", nullable: false),
                    TravelMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puan = table.Column<double>(type: "float", nullable: false),
                    RezNo = table.Column<int>(type: "int", nullable: false),
                    Route_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
