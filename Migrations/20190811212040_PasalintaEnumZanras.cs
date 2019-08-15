using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class PasalintaEnumZanras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zanras",
                table: "Filmai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Zanras",
                table: "Filmai",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Filmai",
                keyColumn: "Id",
                keyValue: 2,
                column: "Zanras",
                value: 1);
        }
    }
}
