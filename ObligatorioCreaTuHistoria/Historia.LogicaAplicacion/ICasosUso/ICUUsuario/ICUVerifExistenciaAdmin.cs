using DTOs.DTO.DTOsUsuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario
{
    public interface ICUVerifExistenciaAdmin
    {
        DTOUsuario Ejecutar(string email, string password);
    }
}
