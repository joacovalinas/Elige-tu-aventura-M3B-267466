using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historia.LogicaNegocios.ValueObjects
{
    [ComplexType]
    public record Contrasena
    {
        [Required]
        public string Pass { get; set; } = string.Empty;
    }
}
