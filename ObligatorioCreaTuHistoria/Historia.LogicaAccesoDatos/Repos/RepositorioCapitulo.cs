using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioCapitulo : IRepositorioCapitulo
    {
        private ApplicationDbContext _context;

        public RepositorioCapitulo(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Capitulo nuevo)
        {
            _context.Capitulos.Add(nuevo);
            _context.SaveChanges();
        }

        public List<Capitulo> FindAll()
        {
            return _context.Capitulos.Include(c => c.CapAnterior).ToList();
        }

        public Capitulo FindById(int id)
        {
            return _context.Capitulos.Include(c => c.CapAnterior).Where(c => c.Id == id).SingleOrDefault();
        }

        public void Remove(Capitulo ToEliminar)
        {
            _context.Capitulos.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Capitulo ToEditar)
        {
            _context.Capitulos.Update(ToEditar);
            _context.SaveChanges();
        }
    }
}
