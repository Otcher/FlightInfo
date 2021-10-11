using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightInfo.Migrations
{
    public partial class new4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airport_City_CityId",
                table: "Airport");

            migrationBuilder.DropIndex(
                name: "IX_Airport_CityId",
                table: "Airport");

            migrationBuilder.AddColumn<int>(
                name: "AirportId",
                table: "City",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_City_AirportId",
                table: "City",
                column: "AirportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_City_Airport_AirportId",
                table: "City",
                column: "AirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Airport_AirportId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_AirportId",
                table: "City");

            migrationBuilder.DropColumn(
                name: "AirportId",
                table: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Airport_CityId",
                table: "Airport",
                column: "CityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Airport_City_CityId",
                table: "Airport",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
