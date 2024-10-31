using Microsoft.EntityFrameworkCore;
using PizzeriaWeb3._1.Models;

namespace PizzeriaWeb3._1.Data
{
    public class PizzeriaContext : DbContext
    {
        public PizzeriaContext(DbContextOptions<PizzeriaContext>options): base (options) 
        {
        }
        
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<PedidoProducto> PedidoProductos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public object PedidoProducto { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedidos>()
                .HasMany(p => p.PedidoProductos)
                .WithOne(pp => pp.Pedido)
                .HasForeignKey(pp => pp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
            // Clave compuesta para la tabla PedidoProducto (relación muchos a muchos)
            modelBuilder.Entity<PedidoProducto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProductoId });

            // Relación entre PedidoProducto y Pedidos
            //modelBuilder.Entity<PedidoProducto>()
            //    .HasOne(pp => pp.Pedido)
            //    .WithMany(p => p.PedidoProductos)
            //    .HasForeignKey(pp => pp.PedidoId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Evitar eliminación en cascada

            // Relación entre PedidoProducto y Productos
            //modelBuilder.Entity<PedidoProducto>()
            //    .HasOne(pp => pp.Producto)
            //    .WithMany(p => p.PedidoProductos)
            //    .HasForeignKey(pp => pp.ProductoId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Evitar eliminación en cascada

            // Relación uno a muchos entre Mesas y Pedidos
            modelBuilder.Entity<Pedidos>()
                .HasOne(p => p.Mesa)
                .WithMany(m => m.Pedidos)
                .HasForeignKey(p => p.MesaId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar eliminación en cascada


            // Relación uno a muchos entre Usuarios y Pedidos (si aplica)
            modelBuilder.Entity<Pedidos>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar eliminación en cascada
            modelBuilder.Entity<Pedidos>()
                .Property(p => p.IdPedidos)
                .ValueGeneratedOnAdd();
        }


    }
}
