using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;

namespace PizzeriaWeb3._1.Controllers
{
    public class CuentaController : Controller
    {
        private readonly PizzeriaContext _context;

        public CuentaController(PizzeriaContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await AutenticacionUsuario(model.Username, model.Password);

                if (usuario != null)
                {
                    if (usuario.Rol.Name == "admin")
                    {
                        return RedirectToAction("Index", "Mesas");
                    }
                    else if (usuario.Rol.Name == "camarero")
                    {
                        return RedirectToAction("Index", "Mesas");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inicio de sesión no válido.");
                }
            }

            return View(model); 
        }


        private async Task<Usuarios> AutenticacionUsuario(string username, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == username && u.Password == password);

            return usuario;
        }

    }
}
