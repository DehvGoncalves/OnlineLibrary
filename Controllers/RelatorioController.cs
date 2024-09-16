using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivros.Services.EmprestimoService;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Services.SessaoService;
using ProjetoEmprestimoLivros.Services.UsuariosService;
using ProjetoEmprestimoLivros.Services.RelatorioService;
using System.Data;
using ProjetoEmprestimoLivros.Models;
using ProjetoEmprestimoLivros.Dto.Relatorio;
using ClosedXML.Excel;
using ProjetoEmprestimoLivros.Filtros;


namespace ProjetoEmprestimoLivros.Controllers
{
    [UsuarioLogado]
    [UsuarioLogadoCliente]
    public class RelatorioController : Controller
    {
        private readonly ISessaoInterface _sessao;
        private readonly ILivroInterface _livroInterface;
        private readonly IUsuariosInterface _usuariosInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        private readonly IRelatorioInterface _relatorioInterface;
        private readonly IMapper _mapper;


        public RelatorioController(ISessaoInterface sessao,
            ILivroInterface livroInterface,
            IUsuariosInterface usuariosInterface,
            IEmprestimoInterface emprestimoInterface,
            IRelatorioInterface relatorioInterface,
            IMapper mapper)

        {
            _sessao = sessao;
            _livroInterface = livroInterface;
            _usuariosInterface = usuariosInterface;
            _emprestimoInterface = emprestimoInterface;
            _relatorioInterface = relatorioInterface;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var sessaoUsuario = _sessao.BuscarSessao(); // Verifica se a sessão está ativa

            if (sessaoUsuario == null) //se o usuário não tiver logado
            {
                TempData["MensagemErro"] = "É necessário fazer login para extrair retórios"; // Mensagem de erro
                return RedirectToAction("Login", "Home");
            }
            if (sessaoUsuario.Perfil != Enum.PerfilEnum.Funcionario) // Se o usuário não for funcionário
            {
                TempData["MensagemErro"] = "Você não tem permissão para acessar essa página"; // Mensagem de erro
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<ActionResult> Gerar(int id) // Vai receber o enum do relatório que queremos gerar
        {
            var tabela = new DataTable(); // Cria uma tabela

            switch (id)
            {
                case 1: //quando não tem Join pode usar o automapper pra mapear tudo de uma vez
                    List<LivroModel> livros = await _livroInterface.BuscarLivros(); // Lista todos os livros
                    List<LivroRelatorioDto> dadosLivros = _mapper.Map<List<LivroRelatorioDto>>(livros); // Mapeia para o DTO

                    if(livros.Count() == 0)
                    {
                        TempData["MensagemErro"] = "Não há livros cadastrados"; // Mensagem de erro
                        return RedirectToAction("Index", "Relatorio");
                    }

                    tabela = _relatorioInterface.ColetarDados(dadosLivros, id); // Coleta os dados e passa o id do relatório
                    break;
                case 2: //Quando tem join, o mapeamento tem que ser feito um por um
                    List<UsuarioModel> clientes = await _usuariosInterface.BuscarUsuarios(0); // Lista todos os usuários do tipo cliente, por isso estamos passando 0 pq isso foi definido lá no enum
                    List<UsuarioRelatorioDto> dadosClientes = new List<UsuarioRelatorioDto>(); // Cria uma lista vazia de DTO, vou usar para mapear um por um devido o join

                    if (clientes.Count > 0)
                    {
                        foreach (var cliente in clientes)
                        {
                            dadosClientes.Add(
                            new UsuarioRelatorioDto
                            {
                                Id = cliente.Id,
                                NomeCompleto = cliente.NomeCompleto,
                                Email = cliente.Email,
                                Status = cliente.Status.ToString(),
                                Perfil = cliente.Perfil.ToString(),
                                Turno = cliente.Turno.ToString(),
                                Logradouro = cliente.Endereco.Logradouro,
                                Numero = cliente.Endereco.Numero,
                                CEP = cliente.Endereco.CEP,
                                Estado = cliente.Endereco.Estado,
                                Complemento = cliente.Endereco.Complemento,
                                DataCadastro = cliente.DataCadastro,
                                DataAlteracao = cliente.DataAlteracao
                            }
                            );
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não há clientes cadastrados"; // Mensagem de erro
                        return RedirectToAction("Index", "Relatorio");
                    }
                    tabela = _relatorioInterface.ColetarDados(dadosClientes, id); // Coleta os dados e passa o id do relatório
                    break;
                case 3: //Quando tem join, o mapeamento tem que ser feito um por um
                    List<UsuarioModel> funcionarios = await _usuariosInterface.BuscarUsuarios(null); // Lista todos os usuários do tipo cliente, por isso estamos passando 0 pq isso foi definido lá no enum
                    List<UsuarioRelatorioDto> dadosFuncionarios = new List<UsuarioRelatorioDto>(); // Cria uma lista vazia de DTO, vou usar para mapear um por um devido o join

                    if (funcionarios.Count > 0)
                    {
                        foreach (var funcionario in funcionarios)
                        {
                            dadosFuncionarios.Add(
                            new UsuarioRelatorioDto
                            {
                                Id = funcionario.Id,
                                NomeCompleto = funcionario.NomeCompleto,
                                Email = funcionario.Email,
                                Status = funcionario.Status.ToString(),
                                Perfil = funcionario.Perfil.ToString(),
                                Turno = funcionario.Turno.ToString(),
                                Logradouro = funcionario.Endereco.Logradouro,
                                Numero = funcionario.Endereco.Numero,
                                CEP = funcionario.Endereco.CEP,
                                Estado = funcionario.Endereco.Estado,
                                Complemento = funcionario.Endereco.Complemento,
                                DataCadastro = funcionario.DataCadastro,
                                DataAlteracao = funcionario.DataAlteracao
                            }
                            );
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não há funcionários cadastrados"; // Mensagem de erro
                        return RedirectToAction("Index", "Relatorio");
                    }

                    tabela = _relatorioInterface.ColetarDados(dadosFuncionarios, id); // Coleta os dados e passa o id do relatório
                    break;
                case 4:
                    List<EmprestimoModel> emprestimosDevolvidos = await _emprestimoInterface.BuscarEmprestimosGeral(null); // Lista todos os empréstimos
                    List<EmprestimoRelatorioDto> dadosEmprestimosDevolvidos = new List<EmprestimoRelatorioDto>();//transforma os emprestimosModel em emprestimosRelatorioDto
                    if (emprestimosDevolvidos.Count > 0)
                    {
                        foreach (var emprestimo in emprestimosDevolvidos)
                        {
                            dadosEmprestimosDevolvidos.Add(
                                new EmprestimoRelatorioDto
                                {
                                    Id = emprestimo.Id,
                                    UsuarioId = emprestimo.UsuarioId,
                                    NomeUsuario = emprestimo.Usuario.NomeCompleto,
                                    Usuario = emprestimo.Usuario.Usuario,
                                    LivroId = emprestimo.LivroId,
                                    Titulo = emprestimo.Livro.Titulo,
                                    ISBN = emprestimo.Livro.ISBN,
                                    DataEmprestimo = emprestimo.DataEmprestimo,
                                    DataDevolucao = (DateTime)emprestimo.DataDevolucao
                                }
                            );
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não há empréstimos devolvidos"; // Mensagem de erro
                        return RedirectToAction("Index", "Relatorio");
                    }
                    tabela = _relatorioInterface.ColetarDados(dadosEmprestimosDevolvidos, id); // Coleta os dados e passa o id do relatório
                    break;
                case 5:
                    List<EmprestimoModel> emprestimosPendentes = await _emprestimoInterface.BuscarEmprestimosGeral("Pendente"); // Lista todos os empréstimos
                    List<EmprestimoRelatorioDto> dadosEmprestimosPendentes = new List<EmprestimoRelatorioDto>();//transforma os emprestimosModel em emprestimosRelatorioDto

                    if (emprestimosPendentes.Count > 0)
                    {
                        foreach (var emprestimo in emprestimosPendentes)
                        {
                            dadosEmprestimosPendentes.Add(
                                new EmprestimoRelatorioDto
                                {
                                    Id = emprestimo.Id,
                                    UsuarioId = emprestimo.UsuarioId,
                                    NomeUsuario = emprestimo.Usuario.NomeCompleto,
                                    Usuario = emprestimo.Usuario.Usuario,
                                    LivroId = emprestimo.LivroId,
                                    Titulo = emprestimo.Livro.Titulo,
                                    ISBN = emprestimo.Livro.ISBN,
                                    DataEmprestimo = emprestimo.DataEmprestimo,
                                    DataDevolucao = emprestimo.DataDevolucao.HasValue ? (DateTime?)emprestimo.DataDevolucao : null
                                }
                            );
                        }

                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não há empréstimos pendentes"; // Mensagem de erro
                        return RedirectToAction("Index", "Relatorio");
                    }
                    tabela = _relatorioInterface.ColetarDados(dadosEmprestimosPendentes, id); // Coleta os dados e passa o id do relatório
                    break;
            }


            // Fazendo com que os dados sejam exportados
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(tabela, "Dados"); // Adiciona os dados que eu peguei do banco de dados no case acima e coloca na planilha
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Dados.xlsx"); // Configuração para download em excel
                }
            }
        }
    }
}
