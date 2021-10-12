using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightInfo.Migrations
{
    public partial class fixedAirport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_AirportId",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_AirportId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "AirportId",
                table: "Flight");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirportId",
                table: "Flight",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirportId",
                table: "Flight",
                column: "AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_AirportId",
                table: "Flight",
                column: "AirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
