using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioRol : IRepositorioRol
    {


        private ApplicationDbContext _context;

        public RepositorioRol(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Rol nuevo)
        {
            _context.Roles.Add(nuevo);
            _context.SaveChanges();

        }

        public List<Rol> FindAll()
        {
            return _context.Roles.Include(u => u.Usuarios).ToList();
        }

        public Rol FindById(int id)
        {
            return _context.Roles.Where(a => a.Id.Equals(id)).SingleOrDefault();
        }

        public Rol FindByNombreRol(string nombreRol)
        {
            Rol u = _context.Roles.FirstOrDefault(u => u.Nombre == nombreRol);
            return u;


        }

        public void Remove(Rol ToEliminar)
        {
            _context.Roles.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Rol ToEditar)
        {
            _context.Roles.Update(ToEditar);
            _context.SaveChanges();
        }
    }
}
