using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.ValueObjects;
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


            // ------------------------------------------------------------------
            // HISTORIA
            // ------------------------------------------------------------------
            modelBuilder.Entity<Historia>(h =>
            {
                h.HasKey(x => x.Id);
                h.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
                h.Property(x => x.Sinopsis).HasMaxLength(2000);
                h.Property(x => x.Estado).IsRequired(); // enum EstadoHistoria, se guarda como int por defecto

                // Uno a uno: la Historia arranca en un capítulo Intermedio puntual.
                // La FK (CapInicialId) va del lado de Historia porque es la entidad "dependiente" de la relación.
                h.HasOne(x => x.CapInicial)
                 .WithOne()
                 .HasForeignKey<Historia>("CapInicialId")
                 .OnDelete(DeleteBehavior.Restrict);

                // Muchos a muchos con Categoria, unidireccional (Categoria no tiene ICollection<Historia>).
                // EF Core genera solo, sin que hagas nada más, una tabla puente (por defecto "HistoriaCategoria").
                h.HasMany(x => x.CatHistorias).WithMany();
            });

            // ------------------------------------------------------------------
            // CAPITULO (TPH) + INTERMEDIO + FINAL
            // Las tres van a la misma tabla física "Capitulos", con una columna
            // discriminadora ("TipoCapitulo") que indica si la fila es Intermedio o Final.
            // ------------------------------------------------------------------
            modelBuilder.Entity<Capitulo>(c =>
            {
                c.HasKey(x => x.Id);
                c.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
                c.Property(x => x.Cuerpo).IsRequired();

                // Auto-referencia: el capítulo anterior siempre es un Intermedio (el único tipo
                // que puede ramificar hacia otros capítulos vía Opcion). Unidireccional: Intermedio
                // no tiene una colección "CapitulosSiguientes" propia, eso ya lo cubre SiguienteCap más abajo.
                c.HasOne(x => x.CapAnterior)
                 .WithMany()
                 .HasForeignKey("CapAnteriorId")
                 .OnDelete(DeleteBehavior.Restrict);

                c.HasDiscriminator<string>("TipoCapitulo")
                 .HasValue<Intermedio>("Intermedio")
                 .HasValue<Final>("Final");
            });

            modelBuilder.Entity<Intermedio>(i =>
            {
                // Uno (Intermedio) a muchos (Opcion): cada opción de ramificación pertenece a un único Intermedio.
                // Restrict (en vez del Cascade por defecto) para no generar un segundo "cascade path" hacia
                // la tabla Capitulos, ya que Opcion también tiene la FK CapDestino apuntando a esa misma tabla.
                i.HasMany(x => x.SiguienteCap)
                 .WithOne()
                 .HasForeignKey("IntermedioId")
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Final>(f =>
            {
                f.Property(x => x.CantidadDeVecesAlcanzado).IsRequired();
            });

            // ------------------------------------------------------------------
            // OPCION
            // ------------------------------------------------------------------
            modelBuilder.Entity<Opcion>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.NombreCap).IsRequired().HasMaxLength(200);

                // Muchos a uno hacia Capitulo (el destino al que lleva esta opción), unidireccional.
                // Restrict por el mismo motivo que arriba: dos FKs (esta y la de Intermedio.SiguienteCap)
                // apuntando a la tabla Capitulos con Cascade en ambas hace que SQL Server rechace el modelo.
                o.HasOne(x => x.CapDestino)
                 .WithMany()
                 .HasForeignKey("CapDestinoId")
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // ------------------------------------------------------------------
            // CATEGORIA
            // ------------------------------------------------------------------
            modelBuilder.Entity<Categoria>(c =>
            {
                c.HasKey(x => x.Id);
                c.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                c.HasIndex(x => x.Nombre).IsUnique();
            });

            // ------------------------------------------------------------------
            // AUDITORIA
            // Todavía no tiene más campos que Id; cuando agregues los que necesites
            // (fecha, acción, usuario responsable, etc.) configuralos acá.
            // ------------------------------------------------------------------
            modelBuilder.Entity<Auditoria>(a =>
            {
                a.HasKey(x => x.Id);
            });

            // ------------------------------------------------------------------
            // ROL
            // ------------------------------------------------------------------
            modelBuilder.Entity<Rol>(r =>
            {
                r.HasKey(x => x.Id);
                r.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
                r.HasIndex(x => x.Nombre).IsUnique();
            });

            // ------------------------------------------------------------------
            // USUARIO
            // ------------------------------------------------------------------
            modelBuilder.Entity<Usuario>(u =>
            {
                u.HasKey(x => x.Id);

                // Complex Type (EF Core 8+): coherente con el [ComplexType] que ya tenés en la clase.
                // NombreCompleto NO tiene identidad ni tabla propia: sus propiedades se guardan
                // como columnas más dentro de la tabla Usuarios. A diferencia de OwnsOne, un complex
                // type siempre viaja junto a su dueño (no se puede consultar ni trackear por separado).
                u.ComplexProperty(x => x.NombreCompleto, nc =>
                {
                    nc.Property(p => p.Nombre).HasColumnName("Nombre").IsRequired().HasMaxLength(100);
                    nc.Property(p => p.Apellido).HasColumnName("Apellido").IsRequired().HasMaxLength(100);
                });

                u.ComplexProperty(x => x.Contrasena, c =>
                {
                    c.Property(p => p.Pass).HasColumnName("Contrasena").IsRequired();
                });

                u.Property(x => x.Email).IsRequired().HasMaxLength(200);
                u.HasIndex(x => x.Email).IsUnique();

                u.Property(x => x.NomUsu).IsRequired().HasMaxLength(100);
                u.HasIndex(x => x.NomUsu).IsUnique();

                // Restrict: no permitir borrar un Rol que todavía tiene usuarios asignados.
                u.HasOne(x => x.Rol)
                 .WithMany(r => r.Usuarios)
                 .HasForeignKey("RolId")
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            });

            // ------------------------------------------------------------------
            // LECTURA
            // ------------------------------------------------------------------
            modelBuilder.Entity<Lectura>(l =>
            {
                l.HasKey(x => x.Id);
                l.Property(x => x.FechaInicio).IsRequired();

                // Usuario dueño de la lectura <-> Usuario.Biblioteca (uno a muchos, bidireccional).
                l.HasOne(x => x.usuario)
                 .WithMany(u => u.Biblioteca)
                 .HasForeignKey("usuarioId")
                 .OnDelete(DeleteBehavior.Restrict);

                // Historia que se está leyendo, unidireccional (Historia no tiene ICollection<Lectura>).
                l.HasOne(x => x.historia)
                 .WithMany()
                 .HasForeignKey("historiaId")
                 .OnDelete(DeleteBehavior.Restrict);

                // CapituloActual y Final apuntan los dos a la tabla Capitulos (TPH):
                // Restrict en ambos por la misma razón de "multiple cascade paths" de arriba.
                l.HasOne(x => x.CapituloActual)
                 .WithMany()
                 .HasForeignKey("CapituloActualId")
                 .OnDelete(DeleteBehavior.Restrict);

                l.HasOne(x => x.Final)
                 .WithMany()
                 .HasForeignKey("FinalId")
                 .OnDelete(DeleteBehavior.Restrict);
            });

            //SEED
            modelBuilder.Entity<Rol>().HasData(
      new Rol { Id = 1, Nombre = "Admin" },
      new Rol { Id = 2, Nombre = "Usuario" }
  );
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
