using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioLectura : IRepositorioLectura
    {
        private ApplicationDbContext _context;

        public RepositorioLectura(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Lectura nuevo)
        {
            _context.Lecturas.Add(nuevo);
            _context.SaveChanges();
        }

        public List<Lectura> FindAll()
        {
            return _context.Lecturas.Include(l => l.Usuario).Include(l => l.Historia).Include(l => l.CapituloActual).ToList();
        }

        public Lectura FindById(int id)
        {
            return _context.Lecturas.Include(l => l.Usuario).Include(l => l.Historia).Include(l => l.CapituloActual).Where(l => l.Id == id).SingleOrDefault();
        }

        public Lectura FindByNombreHistoria(string nombreHistoria)
        {
            return _context.Lecturas.Include(l => l.Usuario).Include(l => l.Historia).Include(l => l.CapituloActual).Where(l => l.Historia.Titulo == nombreHistoria).SingleOrDefault();
        }
        

        public Lectura FindByNombreLectura(string nombreLectura)
        {
            return _context.Lecturas.Include(l => l.Usuario).Include(l => l.Historia).Include(l => l.CapituloActual).Where(l => l.Nombrelectura == nombreLectura).SingleOrDefault();
        }

        public List<Lectura> Obtenerhistorial(int usuarioId, DateTime fechaInicio)
        {
            throw new NotImplementedException();
        }

        public void Remove(Lectura ToEliminar)
        {
            _context.Lecturas.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Lectura ToEditar)
        {
            _context.Lecturas.Update(ToEditar);
            _context.SaveChanges();
        }
    }
}
