using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivros.Migrations
{
    public partial class CorrigindoONomeDaColunaDataDeAlteracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyDataDeAlteracao",
                table: "Livros",
                newName: "DataDeAlteracao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDeAlteracao",
                table: "Livros",
                newName: "MyDataDeAlteracao");
        }
    }
}
