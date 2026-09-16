using System.ComponentModel.DataAnnotations;

namespace Historia.LogicaNegocios.Entidades
{
    public class Lectura
    {
        [Key]
        public int Id { get; set; }

        public Usuario usuario { get; set; }
        public Historia historia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Final Final { get; set; }
        public Capitulo UltCapitulo { get; set; }
    }
}
