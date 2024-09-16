using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.LivroModel> Livros { get; set; }
        public DbSet<Models.UsuarioModel> Usuarios { get; set; }
        public DbSet<Models.EnderecoModel> Enderecos { get; set; }
        public DbSet<Models.EmprestimoModel> Emprestimos { get; set; }

    }
}

