using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioLectura : IRepositorio<Lectura>
    {
        Lectura FindByNombreLectura(string nombreLectura);
        Lectura FindByNombreHistoria(string nombreHistoria);
        public List<Lectura> ObtenerFinalizadas();
        public List<Lectura> Obtenerhistorial(int usuarioId, DateTime? fechaInicio);
    }
}
