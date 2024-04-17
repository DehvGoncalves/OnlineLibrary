using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Services.ViagemService;

namespace ProjetoEmprestimoLivros.Controllers.Viagens
{
    public class ViagensFeitasController : Controller
    {
        private readonly IViagemInterface _viagemInterface;
        public ViagensFeitasController(IViagemInterface viagemInterface)
        {
            _viagemInterface = viagemInterface;
        }
        public async Task<IActionResult> Index()
        {
            var viagens =  await _viagemInterface.ListarViagem();
            return View(viagens);
        }

    }
}


