using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Services.UsuariosService;
using ProjetoEmprestimoLivros.Filtros;

namespace ProjetoEmprestimoLivros.Controllers
{
    [UsuarioLogado] //Só acessa se estiver logado
    [UsuarioLogadoCliente] //Só acessa se estiver logado como cliente
    public class FuncionarioController : Controller
    {
        readonly IUsuariosInterface _usuariosInterface;
        public FuncionarioController(IUsuariosInterface usuarios)
        {
            _usuariosInterface = usuarios;
        }

        public async Task<IActionResult> Index()
        {
            var funcionarios = await _usuariosInterface.BuscarUsuarios(null);
            return View(funcionarios);
        }
    }
}
