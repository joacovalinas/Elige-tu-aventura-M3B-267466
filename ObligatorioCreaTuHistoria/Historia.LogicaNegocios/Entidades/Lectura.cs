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
    
        public Final? Final { get; set; }

        // Donde va quedando parada la lectura (coincide con el ultimo paso de Camino).
        public Capitulo? CapituloActual { get; set; }

        // Camino recorrido: al avanzar se agrega un PasoLectura, al retroceder se quita el ultimo.
        public List<PasoLectura> Camino { get; set; } 

        public void Validar()
        {
            // TODO
        }
    }
}
