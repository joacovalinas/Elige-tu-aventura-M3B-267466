using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using static EscogeAventura.LogicaNegocios.IRepositorios.IRepositorio;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioRol : IRepositorio<Rol>
    {

        Rol FindByNombreRol(string nombreRol);
    }
}
