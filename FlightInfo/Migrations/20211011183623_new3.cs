using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightInfo.Migrations
{
    public partial class new3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airport_City_CityId",
                table: "Airport");

            migrationBuilder.DropIndex(
                name: "IX_Airport_CityId",
                table: "Airport");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Airport",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airport_City_CityId",
                table: "Airport");

            migrationBuilder.DropIndex(
                name: "IX_Airport_CityId",
                table: "Airport");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Airport",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Airport_CityId",
                table: "Airport",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airport_City_CityId",
                table: "Airport",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
