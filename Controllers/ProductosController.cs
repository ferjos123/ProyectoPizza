using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PizzeriaWeb3._1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly PizzeriaContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductosController(PizzeriaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var pizzeriaContext = _context.Productos;
            return View(await pizzeriaContext.ToListAsync());
        }

        public async Task<IActionResult> Carta()
        {
            var pizzeriaContext = _context.Productos;
            return View(await pizzeriaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }
        public async Task<IActionResult> DetalleCarta(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Productos producto, IFormFile? imagen)
        {
            // Solo se validan las propiedades de 'producto', no la imagen
            if (ModelState.IsValid)
            {
                if (imagen != null && imagen.Length > 0)
                {
                    // Manejar el guardado de la imagen
                    var rutaCarpeta = Path.Combine(_env.WebRootPath, "imagenes/productos");
                    var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

                    if (!Directory.Exists(rutaCarpeta))
                    {
                        Directory.CreateDirectory(rutaCarpeta);
                    }

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    // Guardar la ruta de la imagen en la propiedad del modelo
                    producto.ImagenUrl = "/imagenes/productos/" + nombreArchivo;
                }

                // Guardar el producto en la base de datos
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(producto);
        }


        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");

            // Pasar la URL de la imagen existente a la vista (si existe)
            ViewBag.ImagenActual = productos.ImagenUrl;

            return View(productos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,NombreProducto,PrecioProducto,StockProducto,UsuarioId,ImagenUrl")] Productos productos, IFormFile nuevaImagen)
        {
            if (id != productos.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Manejar la carga de una nueva imagen
                    if (nuevaImagen != null && nuevaImagen.Length > 0)
                    {
                        // Obtener la ruta de la carpeta wwwroot/imagenes/productos
                        var rutaCarpeta = Path.Combine(_env.WebRootPath, "imagenes/productos");
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(nuevaImagen.FileName);
                        var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

                        if (!Directory.Exists(rutaCarpeta))
                        {
                            Directory.CreateDirectory(rutaCarpeta);
                        }

                        // Guardar la nueva imagen en el servidor
                        using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                        {
                            await nuevaImagen.CopyToAsync(stream);
                        }

                        // Asignar la nueva ruta de la imagen al producto
                        productos.ImagenUrl = "/imagenes/productos/" + nombreArchivo;
                    }
                    else
                    {
                        // Si no se sube una nueva imagen, mantener la imagen actual
                        productos.ImagenUrl = productos.ImagenUrl; // Mantener la imagen actual
                    }

                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.IdProducto))
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
            return View(productos);
        }


        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            if (productos != null)
            {
                // Eliminar la imagen del servidor si existe
                if (!string.IsNullOrEmpty(productos.ImagenUrl))
                {
                    var rutaImagen = Path.Combine(_env.WebRootPath, productos.ImagenUrl.TrimStart('/'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                }

                _context.Productos.Remove(productos);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
