using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsCapitulo
{
    public class DTOCapitulo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public int HistoriaId { get; set; }


    }
}
