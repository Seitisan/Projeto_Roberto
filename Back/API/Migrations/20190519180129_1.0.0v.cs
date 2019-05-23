using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _100v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coordenador",
                table: "Resposta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocenteMateria",
                table: "Resposta",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDocente",
                table: "Resposta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordenador",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "DocenteMateria",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "IdDocente",
                table: "Resposta");
        }
    }
}
