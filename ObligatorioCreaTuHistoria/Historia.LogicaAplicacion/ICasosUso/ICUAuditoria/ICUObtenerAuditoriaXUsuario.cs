using DTOs.DTO.DTOsAuditoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria
{
    public interface ICUObtenerAuditoriaXUsuario
    {
        public List<DTOAuditoria> ObtenerAuditoriaXUsuario(string NombreUsuario);
    }
}
