using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;


namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioUsuario: IRepositorio<Usuario>
    {
        Usuario FindByNombreUsuario(string nombreUsuario);
        Usuario FindByEmail(string email);
        bool ExisteNombreUsuario(string nombreUsuario);


    }
}
