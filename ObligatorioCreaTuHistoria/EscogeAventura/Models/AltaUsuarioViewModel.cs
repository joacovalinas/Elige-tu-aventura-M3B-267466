using DTOs.DTO.DTOsRol;
using DTOs.DTO.DTOsUsuario;

namespace EscogeAventura.WebApp.Models
{
    public class AltaUsuarioViewModel
    {
        public DTOAltaUsuario Dto { get; set; }
        public List<DTORol> TodosLosRoles = new List<DTORol>();


    }
}
