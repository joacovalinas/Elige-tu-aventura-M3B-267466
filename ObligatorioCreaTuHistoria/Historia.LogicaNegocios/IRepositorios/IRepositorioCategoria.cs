using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioCategoria : IRepositorio<Categoria>
    {
        Categoria FindByNombreCategoria(string nombreCategoria);
        public List<Categoria> ObtenerPorNombres(List<string> nombres);
    }
}
