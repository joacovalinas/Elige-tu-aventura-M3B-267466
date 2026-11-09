using DTOs.DTO.DTOsUsuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario
{
    public interface ICURankingUsuarios
    {
        public List<DTORankingUsuarios> Ejecutar();
    }
}
