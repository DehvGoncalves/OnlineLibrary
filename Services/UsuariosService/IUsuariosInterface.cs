using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Services.UsuariosService
{
    public interface IUsuariosInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
    }
}
