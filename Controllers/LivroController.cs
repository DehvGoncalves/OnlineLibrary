using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Dto.Livro;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Services.SessaoService;
using ProjetoEmprestimoLivros.Filtros;


namespace ProjetoEmprestimoLivros.Controllers
{
    [UsuarioLogado]
    [UsuarioLogadoCliente]
    public class LivroController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILivroInterface _livroInterface;
        private readonly ISessaoInterface _sessaoInterface;
        public LivroController(ILivroInterface livroInterface, IMapper mapper, ISessaoInterface sessaoInterface)
        {
            _livroInterface = livroInterface;
            _mapper = mapper;
            _sessaoInterface = sessaoInterface;
        }
        public async Task<IActionResult> Index()

        {

            var usuarioLogado = _sessaoInterface.BuscarSessao();
            if (usuarioLogado == null)
            {
                TempData["MensagemErro"] = "Faça login para acessar o sistema!";
                return RedirectToAction("Index", "Home");
            }
            if(usuarioLogado.Perfil != Enum.PerfilEnum.Funcionario)
            {
                return RedirectToAction("Index", "Home");
            }
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }

        [HttpGet] // Método para retornar a view de cadastro
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var livro = await _livroInterface.BuscarLivroPorId(id);
                return View(livro);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var livroASerEditado = await _livroInterface.BuscarLivroPorId(id);
                var livroEdicaoDto = _mapper.Map<LivroEdicaoDto>(livroASerEditado);
                return View(livroEdicaoDto);

            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar
            (LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (foto != null)
            {
                if (ModelState.IsValid)//Model que veio é válido? todos os campos vieram?
                {
                    if (!_livroInterface.VerificaSeJaExisteCadastro(livroCriacaoDto))
                    {
                        TempData["MensagemErro"] = "Código ISBN já cadastrado";
                        return View(livroCriacaoDto);
                    }

                    var livro = await _livroInterface.Cadastrar(livroCriacaoDto, foto);

                    TempData["MensagemSucesso"] = "Livro cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Preencher todos os dados!";
                    return View(livroCriacaoDto);
                }
            }
            else
            {
                TempData["MensagemErro"] = "Incluir uma imagem de capa!";
                return View(livroCriacaoDto);
            }
        }

        [HttpPost]
        //lembrando que o nome foto é o mesmo que está no form do html
        public async Task<IActionResult> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                var livro = await _livroInterface.Editar(livroEdicaoDto, foto);

                TempData["MensagemSucesso"] = "Livro editado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Informe os campos de preenhimento obrigatório";
                return View(livroEdicaoDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var livroASerExcluido = await _livroInterface.Excluir(id);
                TempData["MensagemSucesso"] = "Livro excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o livro: " + ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }

}

