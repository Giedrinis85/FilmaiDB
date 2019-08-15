using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class SeedZanrai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Zanrai",
                columns: new[] { "Id", "Pavadinimas" },
                values: new object[] { 1, "Drama" });

            migrationBuilder.InsertData(
                table: "Zanrai",
                columns: new[] { "Id", "Pavadinimas" },
                values: new object[] { 2, "Komedija" });

            migrationBuilder.InsertData(
                table: "Zanrai",
                columns: new[] { "Id", "Pavadinimas" },
                values: new object[] { 3, "Veiksmo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Zanrai",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Zanrai",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zanrai",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
