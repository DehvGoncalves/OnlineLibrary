using Microsoft.AspNetCore.Mvc;

namespace ProjetoEmprestimoLivros.Controllers
{
    public class UsuarioController : Controller
    {

        public IActionResult PerfilUsuario ()
        {
            return View();
        }
        public IActionResult Diario()
        {
            return View();
        }
    }
}
