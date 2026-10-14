using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsHistoria;

namespace EscogeAventura.WebApp.Models
{
    public class AltaCapituloViewModel
    {
        public DTOAltaCapitulo Dto { get; set; }
        public DTOHistoria DtoHistoria { get; set; }
        public List<DTOCapitulo> CapitulosIntermedios { get; set; }
    }
}
