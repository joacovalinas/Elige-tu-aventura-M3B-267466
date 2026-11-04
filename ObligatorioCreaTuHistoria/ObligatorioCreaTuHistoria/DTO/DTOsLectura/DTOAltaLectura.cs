using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsHistoria;
using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsLectura
{
    public class DTOAltaLectura
    {
        public string Nombrelectura { get; set; }
        public int UsuarioId { get; set; }
        public int HistoriaId { get; set; }
        public DateTime FechaInicio { get; set; }

    }
}
