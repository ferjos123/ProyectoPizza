using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models
{
    public class Mesas
    {
        [Key]
        public int IdMesas { get; set; }

        [Required]
        public string NombreMesas { get; set; }

        public ICollection<Pedidos>? Pedidos { get; set; }
    }
}
