using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using ProjetoEmprestimoLivros.Dto;

namespace ProjetoEmprestimoLivros.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;
        private string _caminhoFoto;
        public LivroService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _caminhoFoto = sistema.WebRootPath;
        }
        public async Task<List<LivrosModel>> BuscarLivros()
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

        public async Task<LivrosModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoDaImagem = foto.FileName.Replace(" ", "_").ToLower() + codigoUnico + livroCriacaoDto.ISBN + ".png";

                string caminhoParaSalvarImagens = _caminhoFoto + "\\Imagens\\";
                if (!Directory.Exists(caminhoParaSalvarImagens))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagens);
                }

                using (var strem = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoDaImagem))
                {
                    foto.CopyToAsync(strem).Wait();
                }

                var livro = new LivrosModel
                {
                    ISBN = livroCriacaoDto.ISBN,
                    Titulo = livroCriacaoDto.Titulo,
                    Capa = nomeCaminhoDaImagem,
                    Autor = livroCriacaoDto.Autor,
                    QuantidadeEmEstoque = livroCriacaoDto.QuantidadeEmEstoque,
                    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                    Genero = livroCriacaoDto.Genero,
                    Descricao = livroCriacaoDto.Descricao

                };
                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();
                return livro;
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

        public bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            try
            {
                var LivroBanco = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroCriacaoDto.ISBN); //Se o valor for encontrado ele salva o ISBN Do banco
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
    }
}