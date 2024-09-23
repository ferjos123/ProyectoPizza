using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;

namespace PizzeriaWeb3._1.Controllers
{
    public class PedidosController : Controller
    {
        private readonly PizzeriaContext _context;

        public PedidosController(PizzeriaContext context)
        {
            _context = context;
        }

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
            ViewData["PedidoProductos"] = await _context.PedidoProductos
        .Where(pp => pp.PedidoId == id)
        .ToListAsync(); ;
            return View(pedidos);
        }

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
        public async Task<IActionResult> Delete(int? id)
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

        private bool PedidosExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedidos == id);
        }
    }
}
