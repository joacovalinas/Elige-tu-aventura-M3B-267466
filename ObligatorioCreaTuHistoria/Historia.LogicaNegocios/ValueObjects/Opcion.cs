using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscogeAventura.LogicaNegocios.ValueObjects
{
    
    public class Opcion
    {
        public Opcion()
        {
        }

        [Key]
        public int Id { get; set; }
        public string NombreCap { get; set; }
        public Capitulo CapDestino { get; set; }


    }
}
