using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using System.ComponentModel.DataAnnotations;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Rol
    {
        [Key]

        public int Id { get; set; }
        public string Nombre { get; set; } 

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public Rol()
        {
            
            Usuarios = new List<Usuario>();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatoVacioException("El nombre del rol no puede estar vacío.");
            }
        }



    }
}
