using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivros.Migrations
{
    public partial class AjustandoNomeProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCOmpleto",
                table: "Usuarios",
                newName: "NomeCompleto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "Usuarios",
                newName: "NomeCOmpleto");
        }
    }
}
