using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class SeedFilmTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Filmai",
                columns: new[] { "Id", "Aktoriai", "IsleidimoData", "Pavadinimas", "Zanras" },
                values: new object[] { 1, "Aktorius11, Aktorius12", 2011, "Filmas1", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Filmai",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
