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
    public class CUListarHistoria : ICUListarHistoria
    {
        private  IRepositorioHistoria _repoHistoria;

        public CUListarHistoria(IRepositorioHistoria repoHistoria)
        {
            _repoHistoria = repoHistoria;
        }
        public List<DTOHistoria> Ejecutar()
        {
            List<Historia> historias = _repoHistoria.FindAll();
            return MapperHistoria.ToListDtoHistoria(historias);
        }
    }
}
