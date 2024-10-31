using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;
using PizzeriaWeb3._1.Models.ViewModels;

namespace PizzeriaWeb3._1.Controllers
{
    public class PedidoProductoesController : Controller
    {
        private readonly PizzeriaContext _context;

        public PedidoProductoesController(PizzeriaContext context)
        {
            _context = context;
        }

        // GET: PedidoProductoes
        public async Task<IActionResult> Index()
        {
            var pizzeriaContext = _context.PedidoProductos.Include(p => p.Pedido).Include(p => p.Producto);
            return View(await pizzeriaContext.ToListAsync());
        }

        // GET: PedidoProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoProducto = await _context.PedidoProductos
                .Include(p => p.Pedido)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedidoProducto == null)
            {
                return NotFound();
            }

            return View(pedidoProducto);
        }

        // GET: PedidoProductoes/Create
        public IActionResult Create()
        {
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
            ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");

            ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");


            var model = new PedidoProductoViewModel

            {
                PedidoProductos = new List<ProductoPedidoViewModel>()
            };
            //model.PedidoProductos.Add(new ProductoPedidoViewModel{ProductoId = 1, Cantidad=0, Precio=0.00});
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoProductoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PedidoProductos == null || model.PedidoProductos.Count == 0)
                {
                    ModelState.AddModelError("", "Debes agregar al menos un producto.");
                    ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
                    ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
                    ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
                    return View(model);
                }

                // Crear el pedido sin productos aún
                var pedido = new Pedidos
                {
                    Fecha = DateTime.Now,
                    MesaId = model.MesaId,
                    UsuarioId = model.UsuarioId,
                    Total = 0
                };

                // Agregar el pedido a la base de datos para que se genere el PedidoId
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync(); // Ahora el PedidoId está disponible

                double totalPedido = 0;
                foreach (var item in model.PedidoProductos)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    if (producto != null && producto.StockProducto >= item.Cantidad)
                    {
                        producto.StockProducto -= item.Cantidad;
                        totalPedido += item.Cantidad * producto.PrecioProducto;

                        var pedidoProducto = new PedidoProducto
                        {
                            PedidoId = pedido.IdPedidos, // Aquí puedes usar el PedidoId generado
                            ProductoId = item.ProductoId,
                            Cantidad = item.Cantidad,
                            Precio = producto.PrecioProducto
                        };
                        _context.PedidoProductos.Add(pedidoProducto);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Stock insuficiente para el producto seleccionado.");
                        ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
                        ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
                        ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
                        return View(model);
                    }
                }

                // Actualiza el total del pedido
                pedido.Total = totalPedido;
                _context.Pedidos.Update(pedido); // Asegúrate de actualizar el pedido con el total
                await _context.SaveChangesAsync();

                return RedirectToAction("PedidosCamarero", "Pedidos");
            }

            // Si algo falla, recargamos los datos necesarios para la vista
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
            ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
            ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");

            return View(model);
        }


        // GET: PedidoProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obtener el pedido con sus productos asociados
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProductos)
                .FirstOrDefaultAsync(m => m.IdPedidos == id);

            if (pedido == null)
            {
                return NotFound();
            }

            // Cargar datos para los SelectLists
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
            ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
            ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");

            // Mapear el pedido a un ViewModel para la vista
            var model = new PedidoProductoViewModel
            {
                IdPedidos = pedido.IdPedidos,
                MesaId = pedido.MesaId,
                UsuarioId = pedido.UsuarioId,
                PedidoProductos = pedido.PedidoProductos.Select(pp => new ProductoPedidoViewModel
                {
                    ProductoId = pp.ProductoId ?? 0,
                    Cantidad = pp.Cantidad,
                    Precio = pp.Precio
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PedidoProductoViewModel model)
        {
            if (id != model.IdPedidos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Obtener el pedido original para hacer las actualizaciones
                var pedido = await _context.Pedidos
                    .Include(p => p.PedidoProductos)
                    .FirstOrDefaultAsync(m => m.IdPedidos == id);

                if (pedido == null)
                {
                    return NotFound();
                }

                // Actualizar detalles del pedido
                pedido.MesaId = model.MesaId;
                pedido.UsuarioId = model.UsuarioId;
                pedido.Fecha = DateTime.Now; // Actualiza la fecha de modificación

                double totalPedido = 0;

                // Procesar cada producto en el pedido
                foreach (var item in model.PedidoProductos)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    if (producto != null && producto.StockProducto >= item.Cantidad)
                    {
                        // Buscar si el producto ya estaba en el pedido
                        var pedidoProducto = pedido.PedidoProductos.FirstOrDefault(pp => pp.ProductoId == item.ProductoId);

                        if (pedidoProducto != null)
                        {
                            // Si el producto ya estaba, actualizamos su cantidad y precio
                            producto.StockProducto += pedidoProducto.Cantidad; // Devolver el stock antiguo
                            pedidoProducto.Cantidad = item.Cantidad;
                            producto.StockProducto -= item.Cantidad; // Reducir el stock nuevo
                            pedidoProducto.Precio = producto.PrecioProducto;
                        }
                        else
                        {
                            // Si es un nuevo producto en el pedido
                            producto.StockProducto -= item.Cantidad;

                            pedido.PedidoProductos.Add(new PedidoProducto
                            {
                                PedidoId = pedido.IdPedidos,
                                ProductoId = item.ProductoId,
                                Cantidad = item.Cantidad,
                                Precio = producto.PrecioProducto
                            });
                        }

                        // Calcular el total
                        totalPedido += item.Cantidad * producto.PrecioProducto;
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Stock insuficiente para el producto {producto?.NombreProducto}");
                        ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
                        ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
                        ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
                        return View(model);
                    }
                }

                // Actualizar el total del pedido
                pedido.Total = totalPedido;

                // Guardar los cambios en la base de datos
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedidos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("PedidosCamarero", "Pedidos");
            }

            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NombreProducto");
            ViewData["Mesas"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
            ViewData["Usuarios"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View(model);
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedidos == id);
        }


        // GET: PedidoProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.IdPedidos == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: PedidoProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido != null)
            {
                var pedidoProductos = _context.PedidoProductos.Where(pp => pp.PedidoId == id);
                _context.PedidoProductos.RemoveRange(pedidoProductos); // Eliminar los productos relacionados
                _context.Pedidos.Remove(pedido); // Eliminar el pedido
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // Vuelve al índice o donde desees redirigir
        }


        private bool PedidoProductoExists(int id)
        {
            return _context.PedidoProductos.Any(e => e.PedidoId == id);
        }
    }
}
