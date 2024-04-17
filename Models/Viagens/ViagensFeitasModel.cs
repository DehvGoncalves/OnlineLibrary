using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ProjetoEmprestimoLivros.Models.Viagens
{
    public class ViagensFeitasModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Lugar { get; set; } = string.Empty;        
        [Required]
        public string CompanhiaDeViagem { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public string Hospedagem { get; set; } = string.Empty;
        [Required]
        public double ValorHospedagem { get; set; }
        [Required]
        public int QuantidadePessoas { get; set; }
        [Required]
        public double ValorPorPessoa
        {
            get { return ValorHospedagem / QuantidadePessoas; }
        }

        [Required]
        public string NomePessoas { get; set; } = string.Empty;
        [Required]
        public int Nota { get; set; }
        public DateTime DataUpdate { get; set; } = DateTime.Now;


    }
}

