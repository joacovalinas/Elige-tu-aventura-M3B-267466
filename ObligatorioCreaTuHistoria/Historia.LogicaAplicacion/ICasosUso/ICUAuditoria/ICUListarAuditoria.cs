using DTOs.DTO.DTOsAuditoria;
using DTOs.Mappers;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria
{
    public interface ICUListarAuditoria
    {
        public List<DTOAuditoria> Ejecutar();
        
    }
}
