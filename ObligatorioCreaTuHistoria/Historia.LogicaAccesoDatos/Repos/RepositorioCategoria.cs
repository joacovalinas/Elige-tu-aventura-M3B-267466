using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioCategoria : IRepositorioCategoria
    {

        private ApplicationDbContext _context;

        public RepositorioCategoria(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Categoria nuevo)
        {
            _context.Categorias.Add(nuevo);
            _context.SaveChanges();
        }

        public List<Categoria> FindAll()
        {
            return _context.Categorias.ToList();
        }

        public Categoria FindById(int id)
        {
            return _context.Categorias.Find(id);
        }

        public Categoria FindByNombreCategoria(string nombreCategoria)
        {
            return _context.Categorias.FirstOrDefault(c => c.Nombre == nombreCategoria);
        }


        public List<Categoria> ObtenerPorNombres(List<string> nombres)
        {
            var lista = nombres
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim().ToLower())
                .Distinct()
                .ToList();

            return _context.Categorias
                .Where(c => lista.Contains(c.Nombre.ToLower()))
                .ToList();
        }
        public void Remove(Categoria ToEliminar)
        {
            _context.Categorias.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Categoria ToEditar)
        {
            _context.Categorias.Update(ToEditar);
            _context.SaveChanges();
        }
    }
}
