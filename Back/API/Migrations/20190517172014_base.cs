using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class @base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CadastroPesquisas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoCurso = table.Column<string>(nullable: true),
                    Curso = table.Column<string>(nullable: true),
                    Turma = table.Column<string>(nullable: true),
                    QuantidadeAluno = table.Column<int>(nullable: false),
                    Coordenador = table.Column<string>(nullable: true),
                    PesquisaCriada = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroPesquisas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Texto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perguntas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeDocente = table.Column<string>(nullable: true),
                    Materia = table.Column<string>(nullable: true),
                    CadastroPesquisaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Docentes_CadastroPesquisas_CadastroPesquisaId",
                        column: x => x.CadastroPesquisaId,
                        principalTable: "CadastroPesquisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pesquisas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdCadastroPesquisa = table.Column<int>(nullable: false),
                    NomeAluno = table.Column<string>(nullable: true),
                    Respondido = table.Column<DateTime>(nullable: false),
                    Comentario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesquisas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pesquisas_CadastroPesquisas_IdCadastroPesquisa",
                        column: x => x.IdCadastroPesquisa,
                        principalTable: "CadastroPesquisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resposta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPesquisa = table.Column<int>(nullable: false),
                    IdPergunta = table.Column<int>(nullable: false),
                    ValorResposta = table.Column<int>(nullable: false),
                    ValorImportancia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resposta_Pesquisas_IdPesquisa",
                        column: x => x.IdPesquisa,
                        principalTable: "Pesquisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_CadastroPesquisaId",
                table: "Docentes",
                column: "CadastroPesquisaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pesquisas_IdCadastroPesquisa",
                table: "Pesquisas",
                column: "IdCadastroPesquisa");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_IdPesquisa",
                table: "Resposta",
                column: "IdPesquisa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropTable(
                name: "Perguntas");

            migrationBuilder.DropTable(
                name: "Resposta");

            migrationBuilder.DropTable(
                name: "Pesquisas");

            migrationBuilder.DropTable(
                name: "CadastroPesquisas");
        }
    }
}
