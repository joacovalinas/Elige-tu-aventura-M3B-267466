using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioHistoria : IRepositorio<Historia>
    {
        void Actualizar(Historia historia);
        Historia FindByNombreHistoria(string nombreHistoria);

        

        List<Historia> ObtenerHistoriasPublicadas();
    }
}
