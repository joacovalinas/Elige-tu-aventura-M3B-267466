using EscogeAventura.Cliente.Models.DTOs.DTOsCapitulos;

namespace EscogeAventura.Cliente.Models.DTOsLectura
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
