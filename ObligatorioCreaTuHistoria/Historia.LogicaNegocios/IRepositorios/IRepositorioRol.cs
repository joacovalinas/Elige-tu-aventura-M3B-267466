using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;


namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioRol : IRepositorio<Rol>
    {

        Rol FindByNombreRol(string nombreRol);
    }
}
