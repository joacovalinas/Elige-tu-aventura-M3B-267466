using DTOs.DTO.DTOsCategoria;
using DTOs.DTO.DTOsHistoria;

namespace EscogeAventura.WebApp.Models
{
    public class AltaHistoriaViewModel
    {
        public DTOAltaHistoria Dto { get; set; }
        public List<DTOCategoria> Categorias { get; set; }



    }
}
