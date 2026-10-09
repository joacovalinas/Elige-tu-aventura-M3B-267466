using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsCapitulo
{
    public class DTOAltaCapitulo
    {

        public string Titulo { get; set; }
        public string Cuerpo { get; set; }

        public Intermedio? CapAnterior { get; set; }
        public string tipoCapitulo { get; set; }

        

    }
}
