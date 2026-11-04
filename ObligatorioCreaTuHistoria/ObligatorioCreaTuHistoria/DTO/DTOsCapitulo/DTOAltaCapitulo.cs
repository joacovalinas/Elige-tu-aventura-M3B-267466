using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsCapitulo
{
    public class DTOAltaCapitulo
    {
        public string TipoCapitulo { get; set; }

        public string tipoCapitulo { get; set; }

        public string Titulo { get; set; }
        public string Cuerpo { get; set; }

        public int HistoriaId { get; set; }
        public int AntecedenteId { get; set; }
     
 

    }
}
