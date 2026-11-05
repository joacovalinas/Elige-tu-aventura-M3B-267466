using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscogeAventura.LogicaNegocios.Entidades
{
 

    public class Lectura
    {
       

        public Lectura()
        {

        }

        [Key]

        public int Id { get; set; }
        public string Nombrelectura { get; set; }
        public Usuario Usuario { get; set; }
        public Historia Historia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    
        public bool finalizado { get; set; } = false;
        public Final? Final { get; set; }

        // Donde va quedando parada la lectura (coincide con el ultimo paso de Camino).
        public Capitulo? CapituloActual { get; set; }

        public void Validar()
        {

            if (Usuario == null)
            {
                throw new DominioException("Usuario no valido.");
            }

            if (Historia == null)
            {
                throw new DominioException("Historia no valida.");
            }

            if(string.IsNullOrWhiteSpace(Nombrelectura))
            {
                throw new DominioException("Nombre de lectura no valido.");
            }


        }
    }
}
