using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Home
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Informe o Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a Senha!")]
        public string Senha { get; set; }
    }
}
