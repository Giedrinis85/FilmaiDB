using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class Zanrai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zanrai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pavadinimas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanrai", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Filmai",
                columns: new[] { "Id", "Aktoriai", "IsleidimoData", "Pavadinimas", "Zanras" },
                values: new object[] { 2, "Aktorius21, Aktorius22", 2012, "Filmas2", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zanrai");

            migrationBuilder.DeleteData(
                table: "Filmai",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
