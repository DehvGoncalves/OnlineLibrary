using ProjetoEmprestimoLivros.Models;
using System.Security.Cryptography.X509Certificates;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Models;
using ProjetoEmprestimoLivros.Services.AutenticacaoService;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Services.SessaoService;
using System.Runtime.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ProjetoEmprestimoLivros.Services.EmprestimoService
{
    public class EmprestimoService : IEmprestimoInterface
    {
        private readonly ILivroInterface _livroInterface;
        private readonly AppDbContext _context;
        private readonly ISessaoInterface _sessaoInterface;
        public EmprestimoService(ILivroInterface livroInterface, AppDbContext context, ISessaoInterface sessaoInterface)
        {
            _livroInterface = livroInterface;
            _context = context;
            _sessaoInterface = sessaoInterface;
        }
        public async Task<RespostaModel<EmprestimoModel>> Emprestar(int livroId)
        {
            RespostaModel<EmprestimoModel> resposta = new RespostaModel<EmprestimoModel>();
            try
            {
                var sessaoUsuario = _sessaoInterface.BuscarSessao();
                if (sessaoUsuario == null) //é pq o usuário não está logado
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Você precisa estar logado para realizar um empréstimo!";
                    return resposta;
                } //eu acho que esse trecho é besteira pq já fazemos isso no controller

                var livro = await _livroInterface.BuscarLivroPorId(livroId);

                var emprestimo = new EmprestimoModel
                {//Transformando usuarioModel em emprestimoModel
                    UsuarioId = sessaoUsuario.Id,
                    LivroId = livro.Id,
                    Livro = livro
                };

                _context.Emprestimos.Add(emprestimo);
                await _context.SaveChangesAsync();

                var livroEstoque = await BaixarEstoque(livro);
                resposta.Dados = emprestimo;
                return resposta;

            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
            }
            return resposta;

        }
        public async Task<LivroModel> BaixarEstoque(LivroModel livro)
        {
            livro.QuantidadeEmEstoque--;
            _context.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<LivroModel> RetornarEstoque(LivroModel livro)
        {
            livro.QuantidadeEmEstoque++;
            _context.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<List<EmprestimoModel>> BuscarEmprestimosFiltro(UsuarioModel usuarioSessao, string pesquisar)
        {
            try
            {
                var emprestimosFiltro = await _context.Emprestimos.Include(usuario => usuario.Usuario)
                    .Include(livro => livro.Livro)
                    .Where(emprestimo => emprestimo.UsuarioId == usuarioSessao.Id
                    && emprestimo.Livro.Titulo.Contains(pesquisar)
                    || emprestimo.Livro.Autor.Contains(pesquisar)).ToListAsync();

                return emprestimosFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<List<EmprestimoModel>> BuscarEmprestimos(UsuarioModel usuarioSessao)
        {
            try
            {
                //Quero todos os emprestimos do usuário logado, sem filtro
                //Entramos na tabela de emprestimos, pegamos apenas os emprestimos do usuário logado, e incluimos os dados do livro e do usuario pra dentro desse modelo
                var emprestimosUsuario = await _context.Emprestimos.Where(emprestimo => emprestimo.UsuarioId == usuarioSessao.Id)
                    .Include(livro => livro.Livro)
                    .Include(usuario => usuario.Usuario).ToListAsync();

                return emprestimosUsuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EmprestimoModel> Devolver(int Id)
        {
            try
            {
                var emprestimo = await _context.Emprestimos.Include(livro => livro.Livro).FirstOrDefaultAsync(emprestimo => emprestimo.Id == Id);

                if (emprestimo == null)
                {
                    throw new Exception("Emprestimo não encontrado");
                }

                emprestimo.DataDevolucao = DateTime.Now;
                _context.Update(emprestimo);
                await _context.SaveChangesAsync();

                var livroRetornar = RetornarEstoque(emprestimo.Livro);

                return emprestimo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EmprestimoModel>> BuscarEmprestimos()
        {
            try
            {
                var livrosDevolvidos = await _context.Emprestimos
                    .Include(livro => livro.Livro)
                    .Include(usuario => usuario.Usuario)
                    .Where(emprestimo => emprestimo.DataDevolucao == null).ToListAsync();
                return livrosDevolvidos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EmprestimoModel>> BuscarEmprestimosDevolvidos()
        {
            try
            {
                var livrosDevolvidos = await _context.Emprestimos
                    .Include(livro => livro.Livro)
                    .Include(usuario => usuario.Usuario)
                    .Where(emprestimo => emprestimo.DataDevolucao != null).ToListAsync();
                return livrosDevolvidos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EmprestimoModel>> BuscarEmprestimosPendentes()
        {
            try
            {
                var livrosPendentes = await _context.Emprestimos
                    .Include(livro => livro.Livro)
                    .Include(usuario => usuario.Usuario)
                    .Where(emprestimo => emprestimo.DataDevolucao == null).ToListAsync();
                return livrosPendentes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EmprestimoModel>> BuscarEmprestimosGeral(string tipo = null)
        {
            try
            {
                if(tipo == null)
                {
                    var emprestimosDevolvidos = await BuscarEmprestimosDevolvidos();
                    return emprestimosDevolvidos;
                }
                else
                {
                    var emprestimosPendentes = await BuscarEmprestimosPendentes();
                    return emprestimosPendentes;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
