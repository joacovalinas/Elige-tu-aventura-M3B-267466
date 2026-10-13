using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
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

        public Historia ObtenerPorIdConCapitulos(int id)
        {
         
            return _context.Historias
                .Include(h => h.CapInicial)
                .Include(h => h.Capitulos)
                    .ThenInclude(c => ((Intermedio)c).Opciones)
                .FirstOrDefault(h => h.Id == id);
        }

        public List<Historia> FindAll()
        {
            return _context.Historias.Include(u => u.CapInicial).Include(u => u.CatHistoria).OrderBy(h => h.Titulo).ToList();
        }

        public Historia FindById(int id)
        {
            return _context.Historias.Include(u => u.Estado).Include(u => u.CapInicial).Include(u => u.CatHistoria).Where(a => a.Id.Equals(id)).SingleOrDefault();
        }

        public Historia FindByNombreHistoria(string nombreHistoria)
        {
            return _context.Historias.Include(u => u.Estado).Include(u => u.CapInicial).Include(u => u.CatHistoria).Where(a => a.Titulo.Equals(nombreHistoria)).SingleOrDefault();
        }

        public void Remove(Historia ToEliminar)
        {
            _context.Historias.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Historia ToEditar)
        {
            _context.Historias.Update(ToEditar);
            _context.SaveChanges();
        }
    }
}
