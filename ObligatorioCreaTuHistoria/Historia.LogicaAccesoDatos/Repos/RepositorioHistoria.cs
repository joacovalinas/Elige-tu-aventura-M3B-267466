using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.Enums;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioHistoria : IRepositorioHistoria
    {

        private ApplicationDbContext _context;

        public RepositorioHistoria(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Historia nuevo)
        {
            // Historia.CapInicialId -> Intermedio  y  Intermedio.HistoriaId -> Historia
            // forman un ciclo: EF Core no sabe cuál insertar primero. Se rompe el ciclo
            // en dos guardados, dentro de una transacción para que sea todo o nada.
            using (IDbContextTransaction transaccion = _context.Database.BeginTransaction())
            {
                Intermedio inicial = nuevo.CapInicial;

                // 1) Guardar la historia SIN CapInicial. El capítulo inicial se inserta
                //    igual porque también está en historia.Capitulos, y ya puede
                //    apuntar a la historia (HistoriaId) sin ciclo.
                nuevo.CapInicial = null;
                _context.Historias.Add(nuevo);
                _context.SaveChanges();

                // 2) Con ambos ids ya generados, enlazar CapInicialId y guardar.
                nuevo.CapInicial = inicial;
                _context.SaveChanges();

                transaccion.Commit();
            }
        }

        public void Actualizar(Historia historia)
        {
       
            _context.SaveChanges();
        }

     

        public List<Historia> FindAll()
        {
            return _context.Historias.Include(u => u.CapInicial).Include(u => u.CatHistoria).Where(h => h.activo==true).OrderBy(h => h.Titulo).ToList();
        }

        public Historia FindById(int id)
        {
            return _context.Historias.Include(u => u.CapInicial).Include(u => u.CatHistoria).Include(h => h.Capitulos)
                    .ThenInclude(c => ((Intermedio)c).Opciones).Where(a => a.Id.Equals(id)).SingleOrDefault();
        }

        public Historia FindByNombreHistoria(string nombreHistoria)
        {
            return _context.Historias.Include(u => u.CapInicial).Include(u => u.CatHistoria).Where(a => a.Titulo.Equals(nombreHistoria)).SingleOrDefault();
        }




        public List<Historia> ObtenerHistoriasPublicadas()
        {

            return _context.Historias
                .Include(h => h.CatHistoria)
                .Include(u => u.CapInicial)
                .Where(h => h.activo == true)
                .Where(h => h.Estado == EstadoHistoria.Publicado).ToList();
        }




        public void Remove(Historia ToEliminar)
        {
            Historia historia = FindById(ToEliminar.Id);
            if (historia == null)
                throw new DominioException("Historia no encontrada");

            ToEliminar.activo = false;
            _context.Historias.Update(ToEliminar);
            _context.SaveChanges();


            /*
  
           using var tx = _context.Database.BeginTransaction();
            // 1) Romper el ciclo Historia <-> Capitulo inicial
            historia.CapInicial = null;
            _context.SaveChanges();

            // 2) Borrar opciones, capítulos e historia en un solo SaveChanges.
            //    EF ordena solo los DELETE: opciones -> capítulos -> historia.
            List<Opcion> opciones = historia.Capitulos
                .OfType<Intermedio>()
                .SelectMany(i => i.Opciones)
                .ToList();

            _context.Set<Opcion>().RemoveRange(opciones);
            _context.Set<Capitulo>().RemoveRange(historia.Capitulos);
            _context.Historias.Remove(historia);
            _context.SaveChanges();

            tx.Commit();

            */
        }


        public void Update(Historia ToEditar)
        {
            _context.Historias.Update(ToEditar);
            _context.SaveChanges();
        }

   
    }
}
