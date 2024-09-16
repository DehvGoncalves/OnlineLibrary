using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Dto.Home;
using ProjetoEmprestimoLivros.Models;
using ProjetoEmprestimoLivros.Services.AutenticacaoService;

namespace ProjetoEmprestimoLivros.Services.HomeService
{
    public class HomeService : IHomeInterface
    {

        private readonly AppDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public HomeService(AppDbContext context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
        }
        public async Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDto loginDto)
        {
            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (usuario == null)
                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Credenciais inválidas";    
                    resposta.Status = false;

                 //Se o usuário for nullo significa que o email não existe e no controller vamos tratar isso
                    return resposta; 
                }
                //Senha tá errada?
                if (!_autenticacaoInterface.VerificaLogin(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt)) 

                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Credenciais inválidas";
                    resposta.Status = false;
                   
                    return resposta;   
                }

                resposta.Dados = usuario;
                resposta.Mensagem = "Login Efetuado com sucesso!";
                resposta.Status = true; // Definir como true para indicar sucesso
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
