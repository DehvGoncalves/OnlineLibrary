using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Relatorio
{
    public class LivroRelatorioDto
        //NÃO PODE TER TIPO DE OUTRAS CLASSES, APENAS TIPOS PRIMITIVOS
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public int QuantidadeEmEstoque { get; set; }

    }
}
