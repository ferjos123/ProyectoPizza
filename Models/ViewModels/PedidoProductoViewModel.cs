using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWeb3._1.Models.ViewModels
{
    public class PedidoProductoViewModel
    {
        public int MesaId { get; set; }
        public int UsuarioId { get; set; }
        public List<ProductoPedidoViewModel> PedidoProductos { get; set; } = new List<ProductoPedidoViewModel>();
    }

    public class ProductoPedidoViewModel
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public double? Precio { get; set; }
    }
}
