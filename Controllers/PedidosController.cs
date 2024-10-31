using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
>>>>>>> master

namespace PizzeriaWeb3._1.Controllers
{
    public class PedidosController : Controller
    {
        private readonly PizzeriaContext _context;

        public PedidosController(PizzeriaContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
=======
        public async Task<IActionResult> IndexAdmin()
        {
            var pizzeriaContext = _context.Pedidos.Include(p => p.Mesa).Include(p => p.Usuario);
            return View(await pizzeriaContext.ToListAsync());
        }
>>>>>>> master
        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pizzeriaContext = _context.Pedidos.Include(p => p.Mesa).Include(p => p.Usuario);
            return View(await pizzeriaContext.ToListAsync());
        }
        public async Task<IActionResult> PedidosCamarero()
        {
            var pizzeriaContext = _context.Pedidos.Include(p => p.Mesa).Include(p => p.Usuario);
            return View(await pizzeriaContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdPedidos == id);
            if (pedidos == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
            ViewData["PedidoProductos"] = await _context.PedidoProductos
        .Where(pp => pp.PedidoId == id)
        .ToListAsync(); ;
            return View(pedidos);
        }

=======

            ViewData["PedidoProductos"] = await _context.PedidoProductos
                .Where(pp => pp.PedidoId == id)
                .ToListAsync();

            return View(pedidos);  // Retorna la vista HTML normal
        }

        // Nueva acción para generar el PDF de la misma vista Details
        public async Task<IActionResult> DetailsPdf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdPedidos == id);
            if (pedidos == null)
            {
                return NotFound();
            }

            ViewBag.PedidoProductos = await _context.PedidoProductos
            .Where(pp => pp.PedidoId == id)
            .ToListAsync();

            if (ViewBag.PedidoProductos == null)
            {
                // Manejamos la situación donde no hay productos para este pedido
                ViewBag.PedidoProductos = new List<PedidoProducto>();
            }


            return new ViewAsPdf("Details", pedidos)
            {
                FileName = "ReportePedido.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }



>>>>>>> master
        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["MesaId"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedidos,Fecha,Total,MesaId,UsuarioId")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesaId"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas", pedidos.MesaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", pedidos.UsuarioId);
            return View(pedidos);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return NotFound();
            }
            ViewData["MesaId"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas", pedidos.MesaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", pedidos.UsuarioId);
            return View(pedidos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedidos,Fecha,Total,MesaId,UsuarioId")] Pedidos pedidos)
        {
            if (id != pedidos.IdPedidos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidosExists(pedidos.IdPedidos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesaId"] = new SelectList(_context.Mesas, "IdMesas", "NombreMesas", pedidos.MesaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", pedidos.UsuarioId);
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
<<<<<<< HEAD
        public async Task<IActionResult> Delete(int? id)
=======
        public async Task<IActionResult> Delete(int id)
>>>>>>> master
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdPedidos == id);
            if (pedidos == null)
            {
                return NotFound();
            }

            return View(pedidos);
        }

<<<<<<< HEAD
        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidos = await _context.Pedidos.FindAsync(id);
            if (pedidos != null)
            {
                _context.Pedidos.Remove(pedidos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

=======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProductos) 
                .FirstOrDefaultAsync(p => p.IdPedidos == id);

            if (pedido == null)
            {
                return NotFound();
            }

            if (pedido.PedidoProductos != null && pedido.PedidoProductos.Count > 0)
            {
                _context.PedidoProductos.RemoveRange(pedido.PedidoProductos);
            }

            // Eliminar el pedido
            _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



>>>>>>> master
        private bool PedidosExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedidos == id);
        }
<<<<<<< HEAD
=======

        
>>>>>>> master
    }
}
