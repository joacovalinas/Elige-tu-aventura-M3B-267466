using System.ComponentModel.DataAnnotations.Schema;

namespace Historia.LogicaNegocios.ValueObjects
{
    [ComplexType]
    public record NombreCompleto
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }

        public NombreCompleto()
        {

        }
        public NombreCompleto(string n, string a)
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatoVacioException("El nombre es obligatorio");
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new DatoVacioException("El apellido es obligatorio");
            }

        }
    }
}