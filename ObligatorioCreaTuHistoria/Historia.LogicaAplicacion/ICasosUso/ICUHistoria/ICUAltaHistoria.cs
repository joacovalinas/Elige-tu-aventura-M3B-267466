using DTOs.DTO.DTOsHistoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria
{
    public interface ICUAltaHistoria
    {
        public void Ejecutar(DTOAltaHistoria dto);
    }
}
