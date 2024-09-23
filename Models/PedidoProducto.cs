using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models
{
    public class PedidoProducto
    {
        [Key]
        public int IdPedidoProducto { get; set; }

        [Required]
        public int? PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedidos? Pedido { get; set; }

        [Required]
        public int? ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Productos? Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public double Precio { get; set; }
    }
}
