using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Services.UsuariosService;
using ProjetoEmprestimoLivros.Services.SessaoService;
using ProjetoEmprestimoLivros.Services.EmprestimoService;
using ProjetoEmprestimoLivros.Filtros;

namespace ProjetoEmprestimoLivros.Controllers
{

    [UsuarioLogado] //Só acessa se estiver logado´de acordo com o filtro UsuarioLogado
    public class ClienteController : Controller
        
    {
        private readonly IUsuariosInterface _usuariosInterface;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        public ClienteController(IUsuariosInterface usuariosInterface, ISessaoInterface sessaoIterface, IEmprestimoInterface emprestimoInterface)
        {
            _usuariosInterface = usuariosInterface;
            _sessaoInterface = sessaoIterface;
            _emprestimoInterface = emprestimoInterface;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var clientes = await _usuariosInterface.BuscarUsuarios(id);
            return View(clientes);
        }

        [HttpGet]
        //Método que vai retornar a página de perfil do usuário, fazer filtros e pesquisar

        [UsuarioLogadoAdministrador] //Só acessa se estiver logado como administrador
        public async Task <ActionResult> Perfil (string pesquisar = null, string filtro = "NDevolvidos")
        {
            var sessaoUsuario = _sessaoInterface.BuscarSessao();
            if (sessaoUsuario == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.filtro = filtro; //A View bag vai receber a variável filtro que está sendo passada como parâmetro

            //como padrão a tela mostra os livros não devolvidos

            if(pesquisar != null) //significa que ele digitou algum filtro
            {
                //Preciso da sessaoUsuario pra saber quem está logado e assim usar ele como parâmetro pra retornar os emprestimos
                //E a variavel pesquisar pra saber o que ele digitou
                var emprestimosFiltrado = await _emprestimoInterface.BuscarEmprestimosFiltro(sessaoUsuario, pesquisar);
                return View(emprestimosFiltrado);
            }
            //Porem se o pesquisar for nullo, vamos pegar todos os emprestimos gerais do usuário
            var emprestimosUsuario = await _emprestimoInterface.BuscarEmprestimos(sessaoUsuario);
            return View(emprestimosUsuario);
        }
    }
}
