using AppLanas.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLanas.BD.Data
{
    public class Context : DbContext
    {
        public DbSet<Venta> Ventas => Set<Venta>();

        public DbSet<Producto> Productos => Set<Producto>();

        public DbSet<Caja> Cajas => Set<Caja>();

        public DbSet<ProductoVenta> ProductoVentas => Set<ProductoVenta>();

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>(o =>
            {
                o.Property(b => b.Id);
                o.Property(b => b.idCaja);
                o.Property(b => b.totalGanancia).HasColumnType("Decimal(10,2)");
            });

            modelBuilder.Entity<Producto>(o =>
            {
                o.Property(b => b.id);
                o.Property(b => b.nombreProducto);
                o.Property(b => b.precioProducto).HasColumnType("Decimal(10,2)");
                o.Property(b => b.precioProveedor).HasColumnType("Decimal(10,2)");
                o.Property(b => b.porcentajeGanancia).HasColumnType("Decimal(10,2)");

				//modelBuilder.Entity<Producto>()
				//.Property(b => b.precioProducto)
				//.HasColumnType("Decimal(18,2)");

			});

            modelBuilder.Entity<Caja>(o =>
            {
                o.Property(b => b.Id);

            });
        }
    }
}
