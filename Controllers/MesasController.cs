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
    public class MesasController : Controller
    {
        private readonly PizzeriaContext _context;

        public MesasController(PizzeriaContext context)
        {
            _context = context;
        }

        // GET: Mesas
        public async Task<IActionResult> Index()
        {
            var pizzeriaContext = _context.Mesas;
            return View(await pizzeriaContext.ToListAsync());
        }
        // Get: IndexCamareros
        public async Task<IActionResult> IndexCamareros()
        {
            var pizzeriaContext = _context.Mesas;
            return View(await pizzeriaContext.ToListAsync());
        }

        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var mesas = await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesas == id);
            if (mesas == null)
            {
                return NotFound();
            }

            return View(mesas);
        }

        // GET: Mesas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMesas,NombreMesas,UsuarioId")] Mesas mesas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View(mesas);
        }

        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesas = await _context.Mesas.FindAsync(id);
            if (mesas == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario"/*, mesas.UsuarioId*/);
            return View(mesas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMesas,NombreMesas,UsuarioId")] Mesas mesas)
        {
            if (id != mesas.IdMesas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesasExists(mesas.IdMesas))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View(mesas);
        }

        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesas = await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesas == id);
            if (mesas == null)
            {
                return NotFound();
            }

            return View(mesas);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mesas = await _context.Mesas.FindAsync(id);
            if (mesas != null)
            {
                _context.Mesas.Remove(mesas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesasExists(int id)
        {
            return _context.Mesas.Any(e => e.IdMesas == id);
        }
    }
}
