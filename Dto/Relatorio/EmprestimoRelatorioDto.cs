using ProjetoEmprestimoLivros.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEmprestimoLivros.Dto.Relatorio
{
    public class EmprestimoRelatorioDto
    {
        //Dados que queremos no relatório
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public string Usuario { get; set; }
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime? DataDevolucao { get; set; }
    }
}
