using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioAuditoria : IRepositorio<Auditoria>
    {
        public List<Auditoria> ObtenerPorUsuario(int usuarioId);
        public List<Auditoria> ObtenerPorHistoria(int historiaId);
    }
}
