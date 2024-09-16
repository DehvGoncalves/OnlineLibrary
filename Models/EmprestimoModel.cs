using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoEmprestimoLivros.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        //chave estrangeira pra usuario
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        [JsonIgnore] //Json ignore pra não aparecer no banco de dados
        public UsuarioModel Usuario { get; set; }
        [ForeignKey("Livro")]
        public int LivroId { get; set; }
        [JsonIgnore]
        public LivroModel Livro { get; set; }
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime? DataDevolucao { get; set; }
    }
}
