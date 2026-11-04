using EscogeAventura.Cliente.Models.DTOs.DTOsCapitulos;
using EscogeAventura.Cliente.Models.DTOs.DTOsHistoria;

namespace EscogeAventura.Cliente.Models.DTOs.DTOsLectura
{
    public class DTOAltalectura
    {

        public string Nombrelectura { get; set; }
        public int UsuarioId { get; set; }
        public int HistoriaId { get; set; }
        public DateTime FechaInicio { get; set; }

    }
}
