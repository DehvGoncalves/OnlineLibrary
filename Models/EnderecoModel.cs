using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoEmprestimoLivros.Models
{
    public class EnderecoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Logradouro { get; set; } = string.Empty;
        [Required]
        public string Bairro { get; set; } = string.Empty;
        [Required]
        public string Numero { get; set; } = string.Empty;
        [Required]
        public string CEP { get; set; } = string.Empty;
        [Required]
        public string Estado { get; set; } = string.Empty;
        [Required]
        public string? Complemento { get; set; } = string.Empty;
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }//Chave estrangeira
        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }
    }
}
