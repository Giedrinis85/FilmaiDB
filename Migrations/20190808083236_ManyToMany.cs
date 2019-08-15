using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmaiDB.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pavadinimas",
                table: "Zanrai",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Aktoriai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VardasPavarde = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktoriai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AktoriaiFilmai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AktoriusId = table.Column<int>(nullable: false),
                    FilmasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AktoriaiFilmai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AktoriaiFilmai_Aktoriai_AktoriusId",
                        column: x => x.AktoriusId,
                        principalTable: "Aktoriai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AktoriaiFilmai_Filmai_FilmasId",
                        column: x => x.FilmasId,
                        principalTable: "Filmai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AktoriaiFilmai_AktoriusId",
                table: "AktoriaiFilmai",
                column: "AktoriusId");

            migrationBuilder.CreateIndex(
                name: "IX_AktoriaiFilmai_FilmasId",
                table: "AktoriaiFilmai",
                column: "FilmasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AktoriaiFilmai");

            migrationBuilder.DropTable(
                name: "Aktoriai");

            migrationBuilder.AlterColumn<string>(
                name: "Pavadinimas",
                table: "Zanrai",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
