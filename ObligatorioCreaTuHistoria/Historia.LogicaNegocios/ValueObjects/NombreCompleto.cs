using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public NombreCompleto(string nombre, string apellido)
        {
            validar(nombre, apellido);
            Nombre = nombre;
            Apellido = apellido;
        }


        public void validar(string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new DatoVacioException("El nombre no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new DatoVacioException("El apellido no puede estar vacío.");
            }
        }
    }
}