using DTOs.DTO.DTOsHistoria;
using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria
{
    public interface ICUAltaAuditoria
    {

        public void Ejecutar(string accion, int histId, int usuId);

    }
}
