using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Models
{
    public class LivroModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public string Capa { get; set; } = string.Empty;

        //ISBN é como se fosse um RG do livro, é um número único que identifica o livro
        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public string Autor { get; set; } = string.Empty;
        [Required]
        public string Genero { get; set; } = string.Empty;
        [Required]
        public int AnoPublicacao { get; set; } 
        public List<EmprestimoModel> Emprestimos { get; set; }
        [Required]
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataDeCadastro { get; set;} = DateTime.Now; //Para rastrear a data de cadastro
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now; 
    }
}
