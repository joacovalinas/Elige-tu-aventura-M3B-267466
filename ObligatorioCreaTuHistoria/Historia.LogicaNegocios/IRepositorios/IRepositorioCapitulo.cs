using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;


namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioCapitulo : IRepositorio<Capitulo>
    {
        Capitulo FindByNombreCapitulo(string nombreCapitulo);
        Capitulo FindByNombreCapituloYHistoria(string nombreCapitulo, string nombreHistoria);

    }
}
