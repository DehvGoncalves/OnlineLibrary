using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using System.Globalization;
using ProjetoEmprestimoLivros.Dto.Livro;

namespace ProjetoEmprestimoLivros.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private string _caminhoFoto;

        public LivroService(AppDbContext context, IWebHostEnvironment sistema, IMapper mapper)
        {
            _context = context;
            _caminhoFoto = sistema.WebRootPath;
            _mapper = mapper;
        }
        public async Task<List<LivroModel>> BuscarLivros()
        {
            try
            {
                var livros = await _context.Livros.ToListAsync();
                return livros;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {
                var nomeCaminhoDaImagem = GeraCaminhoArquivo(foto);
                var livro = _mapper.Map<LivroModel>(livroCriacaoDto);
                livro.Capa = nomeCaminhoDaImagem;

                //var livro = new LivrosModel
                //{
                //    ISBN = livroCriacaoDto.ISBN,
                //    Titulo = livroCriacaoDto.Titulo,
                //    Capa = nomeCaminhoDaImagem,
                //    Autor = livroCriacaoDto.Autor,
                //    QuantidadeEmEstoque = livroCriacaoDto.QuantidadeEmEstoque,
                //    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                //    Genero = livroCriacaoDto.Genero,
                //    Descricao = livroCriacaoDto.Descricao

                //};
                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            try
            {
                var LivroBanco = _context.Livros.FirstOrDefault(livros => livros.ISBN == livroCriacaoDto.ISBN); //Se o valor for encontrado ele salva o ISBN Do banco
                if (LivroBanco != null)
                {
                    return false; //Falso pois já existe um livro com esse ISBN
                }
                return true; //Verdadeiro pois não existe um livro com esse ISBN
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ValidaCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            {
                return false; // CPF inválido por não ter 11 dígitos
            }

            // Aqui você pode adicionar mais lógica de validação, como verificar se os dígitos são numéricos, etc.

            return true; // CPF válido
        }

        public async Task<LivroModel> BuscarLivroPorId(int? id)
        {
            try
            {
                var livroASerEditado = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
                return livroASerEditado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto)
        {
            try
            {
                var livro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == livroEdicaoDto.Id);

                var nomeCaminhoDaImagem = ""; // Inicia vazia
                if (foto != null)
                {
                    //Entramos no caminho wwwroot/Imagens e pegamos a capa do livro
                    string caminhoCapaExistente = _caminhoFoto + "\\Imagens\\" + livro.Capa;
                    if (File.Exists(caminhoCapaExistente)) //Se já existe a capa vamos deletar
                    {
                        File.Delete(caminhoCapaExistente);
                    }
                    nomeCaminhoDaImagem = GeraCaminhoArquivo(foto); // Removido 'var' aqui
                }
                var livroModel = _mapper.Map<LivroModel>(livroEdicaoDto);

                if (nomeCaminhoDaImagem != "") // Se o nome da imagem não estiver vazio
                {
                    livroModel.Capa = nomeCaminhoDaImagem;//Salva nova imagem
                }
                else
                {
                    livroModel.Capa = livro.Capa; // Se não tiver imagem nova, mantém a antiga
                }

                livroModel.DataDeAlteracao = DateTime.Now;

                _context.Update(livroModel);
                await _context.SaveChangesAsync();
                return livroModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoDaImagem = foto.FileName.Replace(" ", "_").ToLower()
                + codigoUnico + ".png";

            string caminhoParaSalvarImagens = _caminhoFoto + "\\Imagens\\";

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var strem = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoDaImagem))
            {
                foto.CopyToAsync(strem).Wait();
            }
            return nomeCaminhoDaImagem; //dado que vai salvar no banco

        }

        public async Task<LivroModel> Excluir(int? id)
        {
            try
            {
                var livroASerExcluido = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
                if (id != null)
                {
                    //Remover o livro
                    _context.Livros.Remove(livroASerExcluido);
                    await _context.SaveChangesAsync();
                }
                return livroASerExcluido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}