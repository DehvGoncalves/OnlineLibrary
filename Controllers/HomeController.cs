using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProjetoEmprestimoLivros.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}