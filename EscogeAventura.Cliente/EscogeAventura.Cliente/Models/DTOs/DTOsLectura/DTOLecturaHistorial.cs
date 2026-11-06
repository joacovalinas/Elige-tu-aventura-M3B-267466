namespace EscogeAventura.Cliente.Models.DTOs.DTOsLectura
{
    public class DTOLecturaHistorial
    {
        public int Id { get; set; }
        public string Nombrelectura { get; set; }
        public string NomUsuario { get; set; }
        public string NomHistoria { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public string? Final { get; set; }

        public string? CapituloActual { get; set; }

    }
}
