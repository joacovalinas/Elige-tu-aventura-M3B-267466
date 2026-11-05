using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAccesoDatos.Repos
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {

        private ApplicationDbContext _context;

        public RepositorioAuditoria(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Auditoria nuevo)
        {
            _context.Auditorias.Add(nuevo);
            _context.SaveChanges();
        }

        public List<Auditoria> FindAll()
        {

            return _context.Auditorias.Include(a => a.Historia).Include(a => a.Usuario).ToList();
        }

        public Auditoria FindById(int id)
        {
            return _context.Auditorias.Include(a => a.Historia).Include(a => a.Usuario).Where(a=>a.Id == id).SingleOrDefault();
        }

        public List<Auditoria> ObtenerPorHistoria(int historiaId)
        {
            return _context.Auditorias.Include(a => a.Historia).Include(a => a.Usuario).Where(a => a.Historia.Id == historiaId).ToList();
        }

        public List<Auditoria> ObtenerPorUsuario(int usuarioId)
        {
            return _context.Auditorias.Include(a => a.Historia).Include(a => a.Usuario).Where(a => a.Usuario.Id == usuarioId).ToList();
        }

        public void Remove(Auditoria ToEliminar)
        {
            _context.Auditorias.Remove(ToEliminar);
            _context.SaveChanges();
        }

        public void Update(Auditoria ToEditar)
        {
            _context.Auditorias.Update(ToEditar);
            _context.SaveChanges();
        }
    

    }
}
