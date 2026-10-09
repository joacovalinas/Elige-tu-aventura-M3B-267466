using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                // La FK (CapInicialId, shadow) va del lado de Historia porque es la entidad "dependiente".
                // IsRequired(false): la Historia se inserta ANTES que su primer capítulo (y el capítulo
                // necesita la Historia), así que a nivel de base la FK tiene que poder ser NULL.
                // Que una historia publicada tenga capítulo inicial se valida en Validar(), no acá.
                h.HasOne(x => x.CapInicial)
                 .WithOne()
                 .HasForeignKey<Historia>("CapInicialId")
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);

                // Muchos a muchos con Categoria, unidireccional (Categoria no tiene ICollection<Historia>).
                // EF Core genera solo, sin que hagas nada más, una tabla puente (por defecto "HistoriaCategoria").
                h.HasMany(x => x.CatHistoria).WithMany();
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

                // Cada capítulo pertenece a UNA historia (RF04: al agregar capítulos se indica la historia).
                // FK HistoriaId como shadow property. Es una relación distinta a Historia.CapInicial.
                // Ya no existe CapAnterior: quién lleva a un capítulo se deduce de las Opciones cuyo destino es él.
                c.HasOne(x => x.Historia)
                 .WithMany(h => h.Capitulos)
                 .HasForeignKey("HistoriaId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

                c.HasDiscriminator<string>("TipoCapitulo")
                 .HasValue<Intermedio>("Intermedio")
                 .HasValue<Final>("Final");
            });

            modelBuilder.Entity<Intermedio>(i =>
            {
                // Uno (Intermedio) a muchos (Opcion): cada opción sale de un único Intermedio.
                // Cascade: si se borra el Intermedio se borran sus opciones de salida. No genera "multiple
                // cascade paths" porque la otra FK de Opcion (CapituloDestino) está en Restrict.
                i.HasMany(x => x.Opciones)
                 .WithOne(o => o.CapituloOrigen)
                 .HasForeignKey("CapituloOrigenId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
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
                o.Property(x => x.Texto).IsRequired().HasMaxLength(200);

                // Muchos a uno hacia Capitulo (el destino al que lleva esta opción), unidireccional.
                // Restrict: dos FKs a la tabla Capitulos con Cascade en ambas hace que SQL Server rechace el modelo.
                // (La FK de origen, CapituloOrigenId, se configura arriba en el bloque de Intermedio.)
                // Si agregás Capitulo.OpcionesEntrantes, cambiá .WithMany() por .WithMany(c => c.OpcionesEntrantes).
                o.HasOne(x => x.CapituloDestino)
                 .WithMany()
                 .HasForeignKey("CapituloDestinoId")
                 .IsRequired()
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
                l.Property(x => x.Nombrelectura).HasMaxLength(150);
                l.Property(x => x.FechaInicio).IsRequired();
                // FechaFin es nullable (DateTime?) en la entidad: queda en null mientras la lectura sigue en curso.

                // Usuario dueño de la lectura <-> Usuario.Biblioteca (uno a muchos, bidireccional).
                // A propósito NO hay unique index sobre (UsuarioId, HistoriaId): el mismo usuario puede
                // tener varias Lecturas de la misma Historia (relecturas para llegar a otros finales).
                l.HasOne(x => x.Usuario)
                 .WithMany(u => u.Biblioteca)
                 .HasForeignKey("UsuarioId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                // Historia que se está leyendo, unidireccional (Historia no tiene ICollection<Lectura>).
                l.HasOne(x => x.Historia)
                 .WithMany()
                 .HasForeignKey("HistoriaId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                // CapituloActual y Final apuntan los dos a la tabla Capitulos (TPH) y son opcionales:
                // Final solo tiene valor si la lectura llegó a un final; CapituloActual puede ser null
                // antes de empezar. Restrict en ambos por la razón de "multiple cascade paths".
                l.HasOne(x => x.CapituloActual)
                 .WithMany()
                 .HasForeignKey("CapituloActualId")
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);

                l.HasOne(x => x.Final)
                 .WithMany()
                 .HasForeignKey("FinalId")
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);

                // Camino recorrido en la lectura (para "volver atrás"). Si se borra la Lectura,
                // se borran sus pasos.
                l.HasMany(x => x.Camino)
                 .WithOne(p => p.Lectura)
                 .HasForeignKey("LecturaId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------------------------------------------------
            // PASO LECTURA
            // ------------------------------------------------------------------
            modelBuilder.Entity<PasoLectura>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Orden).IsRequired();

                // Capítulo por el que pasó la lectura, unidireccional. Restrict por "multiple cascade paths".
                p.HasOne(x => x.Capitulo)
                 .WithMany()
                 .HasForeignKey("CapituloId")
                 .IsRequired()
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
        public DbSet<Lectura> Lecturas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


    }
}
