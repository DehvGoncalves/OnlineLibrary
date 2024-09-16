using ProjetoEmprestimoLivros.Dto.Usuario;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Services.UsuariosService
{
    public interface IUsuariosInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
        Task<List<UsuarioModel>> BuscarUsuarios();

        Task<bool> VerificarSeJaExisteUsuario(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioCriacaoDto>Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioModel> BuscarUsuarioPorId(int? id);
        Task<UsuarioModel> MudarStatusUsuario (int? id);
        Task<UsuarioModel> Editar(UsuarioEdicaoDto usuarioEdicaoDto); //Vai tratar e retornar o UsuarioModel
    }
}
