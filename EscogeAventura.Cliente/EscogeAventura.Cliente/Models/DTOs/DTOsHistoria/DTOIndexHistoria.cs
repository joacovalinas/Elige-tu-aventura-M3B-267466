using EscogeAventura.Cliente.Models.DTOs.DTOsCategoria;

namespace EscogeAventura.Cliente.Models.DTOs.DTOsHistoria
{
    public class DTOIndexHistoria
    {

        public List<DTOHistoria> Historias { get; set; } = new List<DTOHistoria>();

        public List<DTOCategoria> categorias { get; set; }


    }
}
