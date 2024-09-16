using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Services.HomeService;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Services.SessaoService;
using ProjetoEmprestimoLivros.Services.EmprestimoService;

namespace ProjetoEmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly ILivroInterface _livroInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        public EmprestimoController(ISessaoInterface sessaoInterface, ILivroInterface livroInterface, 
            IEmprestimoInterface emprestimoInterface)
        {
            _sessaoInterface = sessaoInterface;
            _livroInterface = livroInterface;
            _emprestimoInterface = emprestimoInterface;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Emprestar(int id)
        {
            var sessaoUsuario = _sessaoInterface.BuscarSessao();
            if(sessaoUsuario == null)
            {
                TempData["MensagemErro"] = "Você precisa estar logado para realizar um empréstimo!";
                return RedirectToAction("Login", "Home"); //volta. login da controller Home
            }

            var emprestimo = await _emprestimoInterface.Emprestar(id);
            TempData["MensagemSucesso"] = "Emprestimo realizado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
         public async Task<IActionResult> Devolver(int Id)
        {
            var sessaoUsuario = _sessaoInterface.BuscarSessao();
            if(sessaoUsuario == null)
            {
                TempData["MensagemErro"] = "Você precisa estar logado para realizar uma devolução!";
                return RedirectToAction("Login", "Home");
            }

            var livroDevolucao = await _emprestimoInterface.Devolver(Id);

            TempData["MensagemSucesso"] = "Devolução realizada com sucesso!";
            return RedirectToAction("Perfil", "Cliente");
        }
    }
}


