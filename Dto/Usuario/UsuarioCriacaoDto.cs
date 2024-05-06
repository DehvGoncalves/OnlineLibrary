using ProjetoEmprestimoLivros.Enum;
using ProjetoEmprestimoLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Usuario
{
    public class UsuarioCriacaoDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Digite o nome Completo!")]
        public string NomeCOmpleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o usuario!")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Email!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Status!")]
        public bool Status { get; set; } = true; //Status do usuário, se está ativo ou não, já inicia como ativo
        [Required(ErrorMessage = "Selecione um Perfil!")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Informe o Turno!")]
        public TurnoEnum Turno { get; set; }
        [Required(ErrorMessage = "Informe a Senha!"), MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")] //Verifa se a senha tem no mínimo 6 caracteres
        public string[] Senha { get; set; }
        [Required(ErrorMessage = "Confirme a senha!"), Compare("Senha", ErrorMessage ="As senhas não coincidem")] //Compara se a senha informada é igual a senha
        public string[] ConfirmarSenha { get; set; }
        [Required(ErrorMessage = "Informe o Lagradouro!")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Bairro!")]
        public string Bairro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Número!")]
        public string Numero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o CEP!")]
        public string CEP { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Estado!")]
        public string Estado { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Complemento!")]
        public string? Complemento { get; set; } = string.Empty;
    }
}
