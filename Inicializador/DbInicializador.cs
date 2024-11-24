
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Data;
using PizzeriaWeb3._1.Models;
using System.Drawing.Text;
using System.Security.Cryptography;

namespace PizzeriaWeb3._1.Inicializador
{
    public class DbInicializador : IDbInicializador
    {
        private readonly PizzeriaContext _db;
        //private readonly UserManager<IdentityUser> _userManager; 
        //private readonly RoleManager<IdentityRole> _roleManager;

        public DbInicializador(PizzeriaContext db/*, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager*/) {
            _db = db;
            //_userManager = userManager;
            //_roleManager = roleManager;
        }
        public void inicializar()
{
    try
    {
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate(); // Ejecuta las migraciones pendientes
        }
    }
    catch (Exception)
    {
        throw;
    }

    // Verificar si los roles existen, si no, crearlos
    //if (!_db.Roles.Any(r => r.Name == "Admin"))
    //{
    //    _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
    //    _roleManager.CreateAsync(new IdentityRole("Camarero")).GetAwaiter().GetResult();

    //    // Crear un usuario administrador
    //    var adminUser = new IdentityUser
    //    {
    //        UserName = "AdminUser",
    //        Email = "admin@pizzeria.com",
    //        EmailConfirmed = true
    //    };
    //    _userManager.CreateAsync(adminUser, "AdminPassword123!").GetAwaiter().GetResult();

    //    // Asignar rol al usuario administrador
    //    _userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
    //}
}

    }
}
