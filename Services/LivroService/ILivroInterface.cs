using Microsoft.IdentityModel.Tokens;
using ProjetoEmprestimoLivros.Dto.Livro;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<List<LivroModel>> BuscarLivros();
        Task<List<LivroModel>> BuscarLivrosFiltro(string pesquisar);

        bool ValidaCPF(string cpf);
        bool VerificaSeJaExisteCadastro (LivroCriacaoDto livroCriacaoDto);
        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);

        Task<LivroModel> BuscarLivroPorId(int? id);
        Task<EmprestimoModel> BuscarLivroPorId(int? id, UsuarioModel usuarioSessao);
        Task <LivroModel> Editar (LivroEdicaoDto livroEdicaoDto, IFormFile foto);

        Task<LivroModel> Excluir(int? id);
    }
}
