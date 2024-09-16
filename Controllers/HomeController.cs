using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Dto.Home;
using ProjetoEmprestimoLivros.Services.HomeService;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Services.SessaoService;
using System.Diagnostics;
using ProjetoEmprestimoLivros.Filtros;

namespace ProjetoEmprestimoLivros.Controllers
{
    //Qualquer pessoa pode acessar a home
    public class HomeController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IHomeInterface _homeInterface;
        private readonly ILivroInterface _livroInterface;
        public HomeController(ISessaoInterface sessaoInterface, IHomeInterface homeInterface, ILivroInterface livroInterface)
        {
            _sessaoInterface = sessaoInterface;
            _homeInterface = homeInterface;
            _livroInterface = livroInterface;
        }
        [HttpGet]
        public async Task<ActionResult> Index(string pesquisar = null)
        {
            var UsuarioSessao = _sessaoInterface.BuscarSessao();
            //Se a sessão não existir, o layout padrão é o _Layout (Página deslogada)
            if (UsuarioSessao != null)
            {
                ViewBag.LayoutPagina = "_Layout";
            }
            //Se a sessão existir, o layout padrão é o _LayoutLogado
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";
            }

            //Se o usuário não pesquisar nada, ele verá todos os livros
            if (pesquisar == null)
            {
                var livrosBanco = await _livroInterface.BuscarLivros();
                return View(livrosBanco);
            }
            else
            {
                var livrosBanco = await _livroInterface.BuscarLivrosFiltro(pesquisar);
                return View(livrosBanco);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
        //verificar se o existe sessão de usuário logado
            var usuarioSessao = _sessaoInterface.BuscarSessao();

            //se o usuário estiver logado, ele poderá ver o layout _Layout
            if (usuarioSessao != null)
            {
                ViewBag.UsuarioLogado = usuarioSessao.Id;
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";
            }

            //O metodo BuscarLivroPorId vai variar dependendo se o usuário está logado ou não
            var livro = await _livroInterface.BuscarLivroPorId(id, usuarioSessao);
            if (livro.Usuario != null)
            {
                if (livro.Usuario.Emprestimos == null)
                {
                    ViewBag.Emprestimos = "SemEmprestimo";
                }
            }
            return View(livro);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (_sessaoInterface.BuscarSessao() != null)
            {

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var login = await _homeInterface.RealizarLogin(loginDto);

                if (login.Status == false)
                {
                    TempData["MensagemErro"] = login.Mensagem;
                    return View(login.Dados);
                }
                if (login.Dados.Status == false)
                {
                    TempData["MensagemErro"] = "Procure o suporte para berificar o status da sua conta! ";
                    return View("Login"); //Volta pra tela de login sem nada preenchido
                }

                _sessaoInterface.CriarSessao(login.Dados);
                TempData["MensagemSucesso"] = login.Mensagem;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(loginDto);
            }
        }
        public IActionResult Sair()
        {
            _sessaoInterface.DestruirSessao();
            return RedirectToAction("Index", "Home");
        }
    }
}