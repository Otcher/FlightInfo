using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightInfo.Migrations
{
    public partial class adddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            // Users
            migrationBuilder.InsertData(table: "User",
                columns: new[] { "Name", "Password" },
                values: new object[,]
                {
                    { "oz", "123" },
                    { "yarin", "543" },
                    { "omri", "222" },
                    { "nadav", "1050" }
                });
            */

            // Countries
            migrationBuilder.InsertData(table: "Country",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Israel" },
                    { "Japan" },
                    { "United States" },
                    { "France" }
                });

            // Cities
            migrationBuilder.InsertData(table: "City",
                columns: new[] { "Name", "CountryId" },
                values: new object[,]
                {
                    { "Tel Aviv", 1 },
                    { "Tokyo", 2 },
                    { "Las Vegas", 3 },
                    { "Paris", 4 }
                });

            // Airports
            migrationBuilder.InsertData(table: "Airport",
                columns: new[] { "Name", "CityId", "Latitude", "Longtitude" },
                values: new object[,]
                {
                    { "Tel Aviv airport", 1, 32.005532, 34.88541120000002 },
                    { "Tokyo Haneda Airport", 2, 35.553333, 139.781113 },
                    { "McCarran International Airport", 3, 36.086010, -115.153969 },
                    { "Paris airport", 4, 49.009724, 2.547778 }
                });

            // Planes
            migrationBuilder.InsertData(table: "Plane",
                columns: new[] { "Manufacturer", "Model", "Capacity", "CruiseSpeed" },
                values: new object[,]
                {
                    { "Boeing ", "Boeing 737", 600, 836 },
                    { "Airbus", "Airbus A320", 200, 871 },
                    { "Boeing", "Boeing 787", 290, 954 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            // Users
            migrationBuilder.DeleteData(table: "User",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "oz", "yarin", "omri", "nadav"
                });
            */

            // Countries
            migrationBuilder.DeleteData(table: "Country",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "Israel",
                    "Japan",
                    "United States",
                    "France"
                });

            // Cities
            migrationBuilder.DeleteData(table: "City",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "Tel Aviv",
                    "Tokyo",
                    "Las Vegas",
                    "Paris"
                });

            // Airports
            migrationBuilder.DeleteData(table: "Airport",
               keyColumn: "Name",
               keyValues: new object[]
               {
                    "Tel Aviv airport",
                    "Tokyo Haneda Airport",
                    "McCarran International Airport",
                    "Paris airport"
                });

            // Planes
            migrationBuilder.DeleteData(table: "Plane",
              keyColumn: "Model",
              keyValues: new object[]
               {
                    "Boeing 737",
                    "Airbus A320",
                    "Boeing 787"
                });
        }
    }
}
