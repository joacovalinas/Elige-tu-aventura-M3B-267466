using DTOs.DTO.DTOsLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura
{
    public interface ICUAltaLectura
    {
        public int Ejecutar(DTOAltaLectura dto);
    }
}
