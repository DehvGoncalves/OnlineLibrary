using ProjetoEmprestimoLivros.Dto.Home;
using ProjetoEmprestimoLivros.Models;
using ProjetoEmprestimoLivros.Dto.Home;

namespace ProjetoEmprestimoLivros.Services.HomeService
{
    public interface IHomeInterface
    {
        Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDto loginDto);
    }
}
