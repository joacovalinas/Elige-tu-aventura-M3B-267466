using DTOs.DTO.DTOsAuditoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria
{
    public interface ICUObtenerAuditoriaXHistoria
    {
        public List<DTOAuditoria> ObtenerAuditoriaXHistoria(string NombreHistoria);
    }
}
