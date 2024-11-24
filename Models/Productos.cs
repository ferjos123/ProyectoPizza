using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models
{
    public class Productos
    {

        [Key]
        public int IdProducto { get; set; }

        [Required]
        public string? NombreProducto { get; set; }

        [Required]
        public double PrecioProducto { get; set; }

        [Required]
        public int StockProducto { get; set; }

        public string? ImagenUrl { get; set; }

        public string? Detalle {  get; set; }
        public ICollection<PedidoProducto>? PedidoProductos { get; set; }
    }
}
