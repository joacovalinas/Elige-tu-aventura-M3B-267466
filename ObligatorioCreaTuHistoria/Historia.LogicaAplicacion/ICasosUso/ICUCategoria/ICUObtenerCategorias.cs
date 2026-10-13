
using DTOs.DTO.DTOsCategoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria
{
    public interface ICUObtenerCategorias
    {
        List<DTOCategoria> Ejecutar();
    }
}
