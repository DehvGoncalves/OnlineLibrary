using Microsoft.IdentityModel.Tokens;
using ProjetoEmprestimoLivros.Dto;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<List<LivrosModel>> BuscarLivros();
        bool ValidaCPF(string cpf);
        bool VerificaSeJaExisteCadastro (LivroCriacaoDto livroCriacaoDto);
        Task<LivrosModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
    }
}
