using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Dto.Endereco;
using ProjetoEmprestimoLivros.Dto.Usuario;
using ProjetoEmprestimoLivros.Enum;
using ProjetoEmprestimoLivros.Models;
using ProjetoEmprestimoLivros.Services.UsuariosService;
using ProjetoEmprestimoLivros.Filtros;


namespace ProjetoEmprestimoLivros.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosInterface _usuariosInterface;
        private readonly IMapper _mapper;
        public UsuariosController(IUsuariosInterface usuariosInterface, IMapper mapper)
        {
            _usuariosInterface = usuariosInterface;
            _mapper = mapper;
        }

        [UsuarioLogado]
        [UsuarioLogadoCliente]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuariosInterface.BuscarUsuarios(null);
            return View(usuarios);
        }
        [HttpGet]
        public IActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Funcionario;

            if (id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                if (await _usuariosInterface.VerificarSeJaExisteUsuario(usuarioCriacaoDto))
                {
                    TempData["MensagemErro"] = "O usuário informado já existe, escolha outro!";
                    return View(usuarioCriacaoDto);
                }
                var NovoUsuario = await _usuariosInterface.Cadastrar(usuarioCriacaoDto);
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";

                return RedirectToAction("Login", "Home");
            }
            else
            {//Numero e complemento estão como obrigatório, vetificar
                TempData["MensagemErro"] = "Verifique os dados preenchidos!";
                return View(usuarioCriacaoDto);
            }
        }
        

        [UsuarioLogado]
        [UsuarioLogadoCliente]
        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var usuario = await _usuariosInterface.BuscarUsuarioPorId(id);
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        [UsuarioLogado]
        [UsuarioLogadoAdministrador]
        [HttpPost]
        public async Task<IActionResult> MudarStatusUsuario(UsuarioModel usuario)
        {
            if (usuario.Id != 0 || usuario.Id != null)
            {
                var usuarioSelecionado = await _usuariosInterface.MudarStatusUsuario(usuario.Id);

                if (usuarioSelecionado.Status == true)
                {
                    TempData["MensagemSucesso"] = "Usuario Ativado com sucesso!";
                }
                else
                {
                    TempData["MensagemSucesso"] = "Usuario Cancelado com sucesso!";

                }
                if (usuarioSelecionado.Perfil == PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Cliente", new { Id = "0" });
                }
                else
                {
                    return RedirectToAction("Index", "Funcionario");
                }
            }
            return RedirectToAction("Index");
        }

        [UsuarioLogado]
        [UsuarioLogadoAdministrador]
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var usuarioBanco = await _usuariosInterface.BuscarUsuarioPorId(id);
                var usuario = new UsuarioEdicaoDto
                {
                    Id = usuarioBanco.Id,
                    NomeCompleto = usuarioBanco.NomeCompleto,
                    Email = usuarioBanco.Email,
                    Perfil = usuarioBanco.Perfil,
                    Turno = usuarioBanco.Turno,
                    Usuario = usuarioBanco.Usuario,
                    //usuarioBanco pq foi oque recebemos do metodo buscarUsuarioPorId
                    Endereco = _mapper.Map<EnderecoEditarDto>(usuarioBanco.Endereco)
                };
                return View(usuario);
            }
            return RedirectToAction("Index");
        }

        [UsuarioLogado]
        [UsuarioLogadoAdministrador]
        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuariosInterface.Editar(usuarioEdicaoDto);
                //qndo a operação for feita, a edição já vai ter sido realizada
                TempData["MensagemSucesso"] = "Usuário editado com sucesso!";

                if (usuario.Perfil == PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Cliente", new { Id = "0" });
                }
                else
                {
                    return RedirectToAction("Index", "Funcionario");
                }

            }
            else
            {//Se os dados não estiverem corretos, ele vai retornar a view com os dados que o usuário preencheu
                TempData["MensagemErro"] = "Verifique os dados informados!";
                return View(usuarioEdicaoDto);
            }
        }
    }

}




