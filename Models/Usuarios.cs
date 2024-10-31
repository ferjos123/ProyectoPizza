using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RolId { get; set; }


        [ForeignKey("RolId")]
        public Roles? Rol { get; set; }

        public ICollection<Productos>? Productos { get; set; }

        public ICollection<Pedidos>? Pedidos { get; set; }
    }
}
