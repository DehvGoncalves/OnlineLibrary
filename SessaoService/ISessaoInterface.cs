using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuario);
        void DestruirSessao();
    }
}
