using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorioHistoria : IRepositorio<Historia>
    {
        public void Actualizar(Historia historia);
        public Historia FindByNombreHistoria(string nombreHistoria);
        public List<Historia> ObtenerHistoriasPublicadas(int? CategoriaId);


    }
}
