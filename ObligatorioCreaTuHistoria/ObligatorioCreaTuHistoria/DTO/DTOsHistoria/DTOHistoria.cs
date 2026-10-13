using DTOs.DTO.DTOsCapitulo;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsHistoria
{
    public class DTOHistoria
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Estado { get; set; }
        public List<string> CatHistoria { get; set; }

        public DTOCapitulo CapInicial { get; set; }

        public List<DTOCapitulo> Capitulos { get; set; } 




    }
}
