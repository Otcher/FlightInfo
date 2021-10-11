using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightInfo.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plane_Person_PilotId",
                table: "Plane");

            migrationBuilder.DropIndex(
                name: "IX_Plane_PilotId",
                table: "Plane");

            migrationBuilder.DropColumn(
                name: "PilotId",
                table: "Plane");

            migrationBuilder.DropColumn(
                name: "shit",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "PilotPlane",
                columns: table => new
                {
                    PilotsId = table.Column<int>(type: "int", nullable: false),
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PilotPlane", x => new { x.PilotsId, x.QualificationId });
                    table.ForeignKey(
                        name: "FK_PilotPlane_Person_PilotsId",
                        column: x => x.PilotsId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PilotPlane_Plane_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Plane",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PilotPlane_QualificationId",
                table: "PilotPlane",
                column: "QualificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PilotPlane");

            migrationBuilder.AddColumn<int>(
                name: "PilotId",
                table: "Plane",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shit",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plane_PilotId",
                table: "Plane",
                column: "PilotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plane_Person_PilotId",
                table: "Plane",
                column: "PilotId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
