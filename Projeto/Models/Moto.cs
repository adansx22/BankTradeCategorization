using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    [Table("Moto")]
    public class Moto
    {
        [Column("Id")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int? Ano { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]

        public string? Modelo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string? Placa { get; set; }

        public string? Marca { get; set; }

        public bool Disponivel { get; set; }
    }
}
