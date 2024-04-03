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

        public IActionResult Apresentacao()
        {
            return View();
        }

        public IActionResult PoliticaPrivacidade()
        {
            return View();
        }

    }
}