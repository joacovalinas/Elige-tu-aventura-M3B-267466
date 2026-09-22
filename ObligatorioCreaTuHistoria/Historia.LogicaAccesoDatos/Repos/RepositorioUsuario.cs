using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private ApplicationDbContext _context;

        public RepositorioUsuario(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Usuario nuevo)
        {
            _context.Usuarios.Add(nuevo);
            _context.SaveChanges();

        }

        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            bool u = _context.Usuarios.Any(u => u.NomUsu == nombreUsuario);
            return u;
        }

        public List<Usuario> FindAll()
        {
            return _context.Usuarios.Include(u => u.Rol).ToList();
        }

        public Usuario FindById(int id)
        {
            return _context.Usuarios.Where(a => a.Id.Equals(id)).SingleOrDefault();
        }

        public Usuario FindByNombreUsuario(string nombreUsuario)
        {
            Usuario u =_context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.NomUsu == nombreUsuario);
            return u;
        }

        public void Remove(Usuario ToEliminar)
        {
            _context.Usuarios.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Usuario ToEditar)
        {
            _context.Usuarios.Update(ToEditar);
            _context.SaveChanges();
        }
    }
}
