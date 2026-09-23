using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public abstract class Capitulo
    {
        protected Capitulo()
        {
        }

        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }

        public Intermedio? CapAnterior { get; set; }




    }
}
