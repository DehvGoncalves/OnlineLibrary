using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Models;
using System.Runtime.CompilerServices;

namespace ProjetoEmprestimoLivros.Services.UsuariosService
{
    public class UsuariosService : IUsuariosInterface
    {
        private readonly AppDbContext _context;

        public UsuariosService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();
                
                if(id == 0)
                {
                  registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0).Include(endereco => endereco.Endereco).ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0).Include(endereco => endereco.Endereco).ToListAsync();
                }
                return registros;

            }catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
