using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsHistoria
{
    public class DTOAltaHistoria
    {
        public string Titulo { get; set; }
        public string Sinopsis { get; set; } 
        public List<string> CatHistoria { get; set; }
        public string TituloCapInicial { get; set; } 
        public string CuerpoCapInicial { get; set; } 

    }
}
