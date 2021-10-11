using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightInfo.Migrations
{
    public partial class new6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirportId",
                table: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirportId",
                table: "City",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
