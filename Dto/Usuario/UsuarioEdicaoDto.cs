using ProjetoEmprestimoLivros.Dto.Endereco;
using ProjetoEmprestimoLivros.Enum;
using ProjetoEmprestimoLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Usuario
{
    public class UsuarioEdicaoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Perfil é obrigatório.")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "O campo Turno é obrigatório.")]
        public TurnoEnum Turno { get; set; }
        public EnderecoEditarDto Endereco { get; set; }
    }
}
