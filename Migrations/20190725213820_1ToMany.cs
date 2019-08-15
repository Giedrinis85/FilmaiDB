using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class _1ToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZanraiId",
                table: "Filmai",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Filmai_ZanraiId",
                table: "Filmai",
                column: "ZanraiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmai_Zanrai_ZanraiId",
                table: "Filmai",
                column: "ZanraiId",
                principalTable: "Zanrai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmai_Zanrai_ZanraiId",
                table: "Filmai");

            migrationBuilder.DropIndex(
                name: "IX_Filmai_ZanraiId",
                table: "Filmai");

            migrationBuilder.DropColumn(
                name: "ZanraiId",
                table: "Filmai");
        }
    }
}
