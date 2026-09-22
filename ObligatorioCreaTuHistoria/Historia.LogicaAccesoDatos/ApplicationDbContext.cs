using EscogeAventura.LogicaNegocios.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Lectura>().HasOne(l => l.CapituloActual).WithMany().HasForeignKey("CapituloActualId").OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lectura>().HasOne(l => l.Final).WithMany().HasForeignKey("FinalId").OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lectura>().HasOne(l => l.usuario).WithMany().HasForeignKey("usuarioId");

            modelBuilder.Entity<Lectura>().HasOne(l => l.historia).WithMany().HasForeignKey("historiaId");

            modelBuilder.Entity<Lectura>().HasOne(l => l.usuario).WithMany(u => u.Biblioteca).HasForeignKey("usuarioId");


            modelBuilder.Entity<Usuario>(u =>
            {
                u.HasKey(x => x.Id);
                u.OwnsOne(x => x.NombreCompleto, nc =>
                {
                    nc.Property(p => p.Nombre).HasColumnName("Nombre").IsRequired().HasMaxLength(100);
                    nc.Property(p => p.Apellido).HasColumnName("Apellido").IsRequired().HasMaxLength(100);
                });

                u.OwnsOne(x => x.Contrasena, c =>
                {
                    c.Property(p => p.Pass).HasColumnName("Contrasena").IsRequired();
                });

                u.Property(x => x.Email).IsRequired().HasMaxLength(200);

                u.Property(x => x.NomUsu).IsRequired().HasMaxLength(100);
                u.HasIndex(x => x.NomUsu).IsUnique();

                u.HasOne(x => x.Rol)
                 .WithMany(r => r.Usuarios)
                 .HasForeignKey("RolId")
                 .IsRequired();
            });

            modelBuilder.Entity<Rol>(r =>
            {
                r.HasKey(x => x.Id);
                r.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Rol>().HasIndex(p => new { p.Nombre }).IsUnique();

        }


        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Capitulo> Capitulos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Final> Finales { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public DbSet<Intermedio> Intermedios { get; set; }
        public DbSet<Lectura> lecturas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


    }
}
