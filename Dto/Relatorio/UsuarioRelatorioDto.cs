using ProjetoEmprestimoLivros.Enum;
using ProjetoEmprestimoLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivros.Dto.Relatorio
{
    public class UsuarioRelatorioDto
    {
        //USUARIO
        public int Id { get; set; }

        public string NomeCompleto { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Status { get; set; } //Status do usuário, se está ativo ou não, já inicia como ativo
    
        public string Perfil { get; set; }
        public string Turno { get; set; }
       
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;

        //quando queremos gerar o relatório dos usuários, precisamos dos endereços que estão em outra tabela, então vamos pegar os atributos do endereço e colocar aqui
        //ENDEREÇO
        public string Logradouro { get; set; } = string.Empty;
      
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;


    }
}
