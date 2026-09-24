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
        public Usuario usuario { get; set; }
        public Historia historia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public Final Final { get; set; }
        public Capitulo CapituloActual { get; set; }
    }
}
