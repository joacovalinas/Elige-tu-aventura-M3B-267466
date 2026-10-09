using DTOs.DTO.DTOsCapitulo;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUCapitulo
{
    public interface ICUAltaCapitulo
    {
        public void Ejecutar(DTOAltaCapitulo dto);
    }
}
