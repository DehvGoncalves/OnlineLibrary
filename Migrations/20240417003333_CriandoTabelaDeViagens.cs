using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivros.Migrations
{
    public partial class CriandoTabelaDeViagens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViagensFeitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hospedagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorHospedagem = table.Column<double>(type: "float", nullable: false),
                    QuantidadePessoas = table.Column<int>(type: "int", nullable: false),
                    NomePessoas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViagensFeitas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViagensFeitas");
        }
    }
}
