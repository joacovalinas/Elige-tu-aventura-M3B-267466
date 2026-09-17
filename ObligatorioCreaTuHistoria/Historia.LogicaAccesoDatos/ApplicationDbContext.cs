using EscogeAventura.LogicaNegocios.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
