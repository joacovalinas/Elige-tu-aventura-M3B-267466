using EscogeAventura.Cliente.Models.DTOs.DTOsCapitulos;

namespace EscogeAventura.Cliente.Models.DTOsLectura
{
    public class DTOLeer
    {
        public int Id { get; set; }
        public string nombreLectura { get; set; }
        public string HistoriaTitulo { get; set; }
        public DTOCapituloLectura capituloActual { get; set; }
        public string tipoCapitulo { get; set; }
        public string? opcionElegida { get; set; }

        public DateTime? fechafin { get; set; }
    }
}
