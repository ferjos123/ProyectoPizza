using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;
using System.Diagnostics;

namespace PizzeriaWeb3._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, PizzeriaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Administracion()
        {
            return View();
        }

        public IActionResult Camareros()
        {
            return View();
        }

        public IActionResult Index()
        {
            ViewData["IsLogin"] = true;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private readonly PizzeriaContext _context;
        [HttpPost]
        public async Task<IActionResult> Index(Usuarios model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await AutenticacionUsuario(model.NombreUsuario, model.Password);

                if (usuario != null)
                {
                    if (usuario.Rol.Name == "Admin")
                    {
                        return RedirectToAction("Administracion", "Home"); 
                    }
                    else if (usuario.Rol.Name == "Camarero")
                    {
                        return RedirectToAction("Camareros", "Home"); 
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inicio de sesión no válido.");
<<<<<<< HEAD
=======
                    HttpContext.Response.StatusCode = 401;
>>>>>>> master
                }
            }

            ViewData["IsLogin"] = true; 
            return View(model); 
        }

        private async Task<Usuarios> AutenticacionUsuario(string username, string password)
        {
            var usuario = await _context.Usuarios
        .Include(u => u.Rol) 
        .FirstOrDefaultAsync(u => u.NombreUsuario == username && u.Password == password);

            return usuario;
        }
    }
}
