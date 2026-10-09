using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class PasoLectura
    {

        public PasoLectura()
        {
        }

        [Key]
        public int Id { get; set; }

        // FKs (LecturaId y CapituloId) como shadow properties.
        public Lectura Lectura { get; set; }
        public Capitulo Capitulo { get; set; }

        public int Orden { get; set; }


    }
}
