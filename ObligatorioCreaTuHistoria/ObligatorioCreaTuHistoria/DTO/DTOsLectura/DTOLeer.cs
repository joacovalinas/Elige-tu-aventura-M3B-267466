using DTOs.DTO.DTOsCapitulo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsLectura
{
    public class DTOLeer
    {
        public string nombreLectura { get; set; }
        public string HistoriaTitulo { get; set; }
        public DTOCapitulo capituloActual { get; set; }
        public string tipoCapitulo { get; set; }
        public int? opcionElegida { get; set; }

        public DateTime? fechafin { get; set; }

    }
}
