using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoscowWeather.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirTemperature = table.Column<float>(type: "real", nullable: false),
                    DewPoint = table.Column<float>(type: "real", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    DirectionWind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeedWind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloudBase = table.Column<int>(type: "int", nullable: false),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: false),
                    WeatherConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rel_humidity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cloudy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
