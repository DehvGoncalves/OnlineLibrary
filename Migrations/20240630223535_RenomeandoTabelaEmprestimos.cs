using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivros.Migrations
{
    public partial class RenomeandoTabelaEmprestimos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmprestimoModel_Livros_LivroId",
                table: "EmprestimoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmprestimoModel_Usuarios_UsuarioId",
                table: "EmprestimoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmprestimoModel",
                table: "EmprestimoModel");

            migrationBuilder.RenameTable(
                name: "EmprestimoModel",
                newName: "Emprestimos");

            migrationBuilder.RenameIndex(
                name: "IX_EmprestimoModel_UsuarioId",
                table: "Emprestimos",
                newName: "IX_Emprestimos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_EmprestimoModel_LivroId",
                table: "Emprestimos",
                newName: "IX_Emprestimos_LivroId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emprestimos",
                table: "Emprestimos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Usuarios_UsuarioId",
                table: "Emprestimos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Usuarios_UsuarioId",
                table: "Emprestimos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emprestimos",
                table: "Emprestimos");

            migrationBuilder.RenameTable(
                name: "Emprestimos",
                newName: "EmprestimoModel");

            migrationBuilder.RenameIndex(
                name: "IX_Emprestimos_UsuarioId",
                table: "EmprestimoModel",
                newName: "IX_EmprestimoModel_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Emprestimos_LivroId",
                table: "EmprestimoModel",
                newName: "IX_EmprestimoModel_LivroId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmprestimoModel",
                table: "EmprestimoModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmprestimoModel_Livros_LivroId",
                table: "EmprestimoModel",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmprestimoModel_Usuarios_UsuarioId",
                table: "EmprestimoModel",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
