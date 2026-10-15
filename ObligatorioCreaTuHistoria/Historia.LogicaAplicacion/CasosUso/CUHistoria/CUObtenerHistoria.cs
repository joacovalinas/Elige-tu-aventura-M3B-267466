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
    public class CUObtenerHistoria: ICUObtenerHistoria
    {
        private IRepositorioHistoria _repoHistoria;
        public CUObtenerHistoria(IRepositorioHistoria repoHistoria)
        {
            _repoHistoria = repoHistoria;
        }
        public DTOHistoria Ejecutar(int id)
        {
            Historia historia = _repoHistoria.FindById(id);
            DTOHistoria ret = MapperHistoria.ToDtoHistoria(historia);
            return ret;
        }

    }
}
