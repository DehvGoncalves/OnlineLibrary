using ProjetoEmprestimoLivros.Models.Viagens;

namespace ProjetoEmprestimoLivros.Services.ViagemService
{
    public interface IViagemInterface
    {
        Task<List<ViagensFeitasModel>> ListarViagem();
    }
}
