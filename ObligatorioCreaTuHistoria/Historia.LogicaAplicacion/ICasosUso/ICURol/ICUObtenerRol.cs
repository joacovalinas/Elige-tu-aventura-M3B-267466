using DTOs.DTO.DTOsRol;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICURol
{
    public interface ICUObtenerRol
    {
        public List<DTORol> Ejecutar();
    }
}
