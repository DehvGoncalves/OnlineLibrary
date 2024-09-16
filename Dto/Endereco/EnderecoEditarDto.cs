using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Endereco
{
    public class EnderecoEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é Logradouro.")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Nome é Bairro.")]
        public string Bairro { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Nome é Cidade.")]
        public string Numero { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Nome é CEP.")]
        public string CEP { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Nome é Estado.")]
        public string Estado { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
        public int UsuarioId { get; set; } //Chave estrangeira de Usuario
    }
}
