using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscogeAventura.LogicaNegocios.ValueObjects
{
    [ComplexType]
    public record Contrasena
    {
        [Required]
        public string Pass { get; set; }

        public Contrasena()
        {
        }

        public Contrasena(string pass)
        {
            validar(pass);
            Pass = pass;
        }

        public void validar(string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                throw new DatoVacioException("La contraseña no puede estar vacía.");
            }
        }
    }
}
