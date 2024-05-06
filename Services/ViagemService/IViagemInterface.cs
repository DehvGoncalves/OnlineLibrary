using ProjetoEmprestimoLivros.Dto.ViagemDto;
using ProjetoEmprestimoLivros.Models.Viagens;

namespace ProjetoEmprestimoLivros.Services.ViagemService
{
    public interface IViagemInterface
    {
        Task<List<ViagensFeitasModel>> ListarViagem();
        Task<ViagensFeitasModel> CadastrarViagem(ViagemCadastroDto cadastrarViagemDto, IFormFile foto_lugar);

        Task<ViagensFeitasModel> BuscarLivroPorId (int? id);
    }

}
