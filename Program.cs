using Microsoft.Extensions.Options;
using PizzeriaWeb3._1.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PizzeriaContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("Pizzeria2DB")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
<<<<<<< HEAD
app.UseStaticFiles();
=======
//agregue esto por el pfd, el use static ya estaba
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Rotativa");
app.UseStaticFiles();
app.MapDefaultControllerRoute();
>>>>>>> master

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
