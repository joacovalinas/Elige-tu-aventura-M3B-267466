using System.ComponentModel.DataAnnotations.Schema;

namespace EscogeAventura.LogicaNegocios.ValueObjects
{
    [ComplexType]
    public record NombreCompleto
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }

        public NombreCompleto()
        {

        }
        
    }
}