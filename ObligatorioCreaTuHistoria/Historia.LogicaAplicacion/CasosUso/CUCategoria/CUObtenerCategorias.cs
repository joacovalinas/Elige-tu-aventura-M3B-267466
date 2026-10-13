using DTOs.DTO.DTOsCategoria;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUCategoria
{
    public class CUObtenerCategorias : ICUObtenerCategorias
    {
        private IRepositorioCategoria _repoCategoria;

        public CUObtenerCategorias(IRepositorioCategoria repoCategoria)
        {
            _repoCategoria = repoCategoria;
        }

        public List<DTOCategoria> Ejecutar()
        {
           List<Categoria> categorias = _repoCategoria.FindAll();
           List<DTOCategoria> dtoCategorias = MapperCategoria.ToListDtoCategoria(categorias);

            return dtoCategorias;

        }
    }
}
