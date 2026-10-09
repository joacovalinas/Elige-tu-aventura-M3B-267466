using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
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
            _context.Historias.Add(nuevo);
            _context.SaveChanges();
        }

        public List<Historia> FindAll()
        {
            return _context.Historias.Include(u => u.Estado).Include(u => u.CapInicial).Include(u => u.CatHistoria).ToList();
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
