using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHealth1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doenca",
                columns: table => new
                {
                    NrCid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sintomas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doenca", x => x.NrCid);
                });

            migrationBuilder.CreateTable(
                name: "Localizacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    CrmId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.CrmId);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SintomasPaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doenca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicoCrmId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalizacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnostico_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnostico_Medico_MedicoCrmId",
                        column: x => x.MedicoCrmId,
                        principalTable: "Medico",
                        principalColumn: "CrmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalheDiagnostico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamesSolicitados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomePaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosticoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheDiagnostico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalheDiagnostico_Diagnostico_DiagnosticoId",
                        column: x => x.DiagnosticoId,
                        principalTable: "Diagnostico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticoDoenca",
                columns: table => new
                {
                    diagnosticoId = table.Column<int>(type: "int", nullable: false),
                    doencaNrCid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticoDoenca", x => new { x.diagnosticoId, x.doencaNrCid });
                    table.ForeignKey(
                        name: "FK_DiagnosticoDoenca_Diagnostico_diagnosticoId",
                        column: x => x.diagnosticoId,
                        principalTable: "Diagnostico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticoDoenca_Doenca_doencaNrCid",
                        column: x => x.doencaNrCid,
                        principalTable: "Doenca",
                        principalColumn: "NrCid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalheDiagnostico_DiagnosticoId",
                table: "DetalheDiagnostico",
                column: "DiagnosticoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostico_LocalizacaoId",
                table: "Diagnostico",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostico_MedicoCrmId",
                table: "Diagnostico",
                column: "MedicoCrmId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoDoenca_doencaNrCid",
                table: "DiagnosticoDoenca",
                column: "doencaNrCid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheDiagnostico");

            migrationBuilder.DropTable(
                name: "DiagnosticoDoenca");

            migrationBuilder.DropTable(
                name: "Diagnostico");

            migrationBuilder.DropTable(
                name: "Doenca");

            migrationBuilder.DropTable(
                name: "Localizacao");

            migrationBuilder.DropTable(
                name: "Medico");
        }
    }
}
