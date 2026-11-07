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
       // Lectura FindByNombreUsuarioLecturaHistoria(string nombreUsuario,string nombreHistoria, );
        public List<Lectura> Obtenerhistorial(int usuarioId, DateTime? fechaInicio);
    }
}
