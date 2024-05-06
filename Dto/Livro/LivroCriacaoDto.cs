using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Livro
{
    public class LivroCriacaoDto
    {
        [Required(ErrorMessage = "Insira o Titulo")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira a Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira o ISBM")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o Autor")]
        public string Autor { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o Genero")]
        public string Genero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o ano de publicação")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "Insira a quantidade em estoque")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
