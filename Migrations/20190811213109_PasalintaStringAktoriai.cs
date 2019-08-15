using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class PasalintaStringAktoriai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktoriai",
                table: "Filmai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aktoriai",
                table: "Filmai",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Filmai",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aktoriai",
                value: "Aktorius11, Aktorius12");

            migrationBuilder.UpdateData(
                table: "Filmai",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aktoriai",
                value: "Aktorius21, Aktorius22");
        }
    }
}
