using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Services.EmprestimoService
{
    public interface IEmprestimoInterface
    {
        //Resposta Model é oque nos ajuda a mostrar mensagens específicas para o usuário
        Task<RespostaModel<EmprestimoModel>> Emprestar(int livroId); //Vai ser do tipo RespostaModel e esse resposta model vai ser do tipo EmprestimoModel que recebe um livroId
        Task<List<EmprestimoModel>> BuscarEmprestimosFiltro(UsuarioModel usuarioSessao, string pesquisar); //Vai retornar uma lista de emprestimos
        Task<List<EmprestimoModel>> BuscarEmprestimos (UsuarioModel usuarioSessao); //Vai retornar uma lista de emprestimos de um usuário específico
        Task<List<EmprestimoModel>> BuscarEmprestimos (); //Vai retornar uma lista de emprestimos de um usuário específico
        Task<EmprestimoModel> Devolver(int idEmprestimo); //Vai devolver um emprestimo
        Task<List<EmprestimoModel>> BuscarEmprestimosDevolvidos(); //Vai retornar uma lista de emprestimos devolvidos
        Task<List<EmprestimoModel>> BuscarEmprestimosPendentes(); //Vai retornar uma lista de emprestimos pendentes
        Task<List<EmprestimoModel>> BuscarEmprestimosGeral(string tipo = null);
    }
}
