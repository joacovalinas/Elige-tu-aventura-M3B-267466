using DTOs.DTO.DTOsRol;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICURol;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CURol
{
    public class CUObtenerRol : ICUObtenerRol
    {

        private IRepositorioRol _repoRol;

        public CUObtenerRol(IRepositorioRol repoRol)
        {
            _repoRol = repoRol;

        }


        public List<DTORol> Ejecutar()
        {
            List<Rol> todosLosRoles = _repoRol.FindAll();
            List<DTORol> ret = MapperRol.ToListDtoRol(todosLosRoles);
            return ret;
        }
    }
}
