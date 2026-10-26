using DTOs.DTO.DTOsLectura;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CULectura
{
    public class CUObtenerLectura : ICUObtenerLectura
    {
        private  IRepositorioLectura _repositorioLectura;

        public CUObtenerLectura(IRepositorioLectura repositorioLectura)
        {
            _repositorioLectura = repositorioLectura;
        }
        public DTOLectura Ejecutar(string NombreLectura)
        {
            DTOLectura lectura = MapperLectura.ToDtoLectura(_repositorioLectura.FindByNombreLectura(NombreLectura));

            return lectura;
        }
    }
}
