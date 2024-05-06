using ProjetoEmprestimoLivros.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeCOmpleto { get; set; } = string.Empty;
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; } = true; //Status do usuário, se está ativo ou não, já inicia como ativo
        [Required]
        public PerfilEnum Perfil { get; set; }
        [Required]
        public TurnoEnum Turno { get; set; }
        [Required]
        public byte[] SenhaHash { get; set; } //Senha criptografada
        [Required]
        public byte[] SenhaSalt { get; set; } //Chave de criptografia
        [Required]
        public EnderecoModel Endereco { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;

    }

}
