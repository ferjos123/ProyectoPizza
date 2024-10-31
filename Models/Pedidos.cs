using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models
{
    public class Pedidos
    {
        [Key]
        public int IdPedidos { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public int MesaId { get; set; }
        [ForeignKey("MesaId")]
        public Mesas? Mesa { get; set; }

        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios? Usuario { get; set; }

        public ICollection<PedidoProducto> PedidoProductos { get; set; }
    }
}
