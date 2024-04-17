using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Dto;
using ProjetoEmprestimoLivros.Services.LivroService;

namespace ProjetoEmprestimoLivros.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }
        public async Task<IActionResult> Index()
        {
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }

        [HttpGet] // Método para retornar a view de cadastro
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (foto != null)
            {
                if (ModelState.IsValid)//Model que veio é válido? todos os campos vieram?
                {
                    if (!_livroInterface.VerificaSeJaExisteCadastro(livroCriacaoDto))
                    {
                        return View(livroCriacaoDto);
                    }
                    var livro = await _livroInterface.Cadastrar(livroCriacaoDto, foto);

                    // Retorna algum resultado de sucesso, como redirecionar para outra página
                    return RedirectToAction("Index"); // Por exemplo, redireciona para a página principal dos livros após o cadastro bem-sucedido
                }
                else
                {
                    return View(livroCriacaoDto);
                }
            }
            else
            {
                return View(livroCriacaoDto);
            }
        }

    }
}


