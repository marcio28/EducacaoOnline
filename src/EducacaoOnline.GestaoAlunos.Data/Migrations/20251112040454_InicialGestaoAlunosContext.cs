using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace EducacaoOnline.GestaoAlunos.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class InicialGestaoAlunosContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdAluno = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCurso = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoAprendizado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMatricula = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAprendizado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    HistoricoAprendizadoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CertificadoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matricula_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matricula_Certificado_CertificadoId",
                        column: x => x.CertificadoId,
                        principalTable: "Certificado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matricula_HistoricoAprendizado_HistoricoAprendizadoId",
                        column: x => x.HistoricoAprendizadoId,
                        principalTable: "HistoricoAprendizado",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_AlunoId",
                table: "Matricula",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_CertificadoId",
                table: "Matricula",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_HistoricoAprendizadoId",
                table: "Matricula",
                column: "HistoricoAprendizadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Certificado");

            migrationBuilder.DropTable(
                name: "HistoricoAprendizado");
        }
    }
}
