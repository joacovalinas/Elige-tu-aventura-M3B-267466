using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsHistoria;
using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsLectura
{
    public class DTOLectura
    {

        public int Id { get; set; }
        public string Nombrelectura { get; set; }
        public DTOUsuario Usuario { get; set; }
        public DTOHistoria Historia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public DTOCapitulo? Final { get; set; }

        // Donde va quedando parada la lectura (coincide con el ultimo paso de Camino).
        public DTOCapitulo? CapituloActual { get; set; }


    }
}
