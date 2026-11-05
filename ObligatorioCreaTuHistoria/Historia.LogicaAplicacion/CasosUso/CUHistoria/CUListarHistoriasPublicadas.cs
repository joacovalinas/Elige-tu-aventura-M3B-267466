using DTOs.DTO.DTOsHistoria;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria
{
    public class CUListarHistoriasPublicadas :ICUListarHistoriasPublicadas
    {

        private IRepositorioHistoria _repoHistoria;

        public CUListarHistoriasPublicadas(IRepositorioHistoria repoHistoria)
        {
            _repoHistoria = repoHistoria;
        }
        public List<DTOHistoria> Ejecutar(int? CategoriaId)
        {


            List<Historia> historias = _repoHistoria.ObtenerHistoriasPublicadas(CategoriaId ?? 0);
            return MapperHistoria.ToListDtoHistoria(historias);
        }
    }
}
