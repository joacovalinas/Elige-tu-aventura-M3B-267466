using EscogeAventura.Cliente.Models.DTOs.DTOsCategoria;

namespace EscogeAventura.Cliente.Models.DTOs.DTOsHistoria
{
    public class DTOIndexHistoria
    {

        public List<DTOHistoria> Historias { get; set; } 
        public List<DTOCategoria> Categorias { get; set; } 
        public int? CategoriaId { get; set; }


    }
}
