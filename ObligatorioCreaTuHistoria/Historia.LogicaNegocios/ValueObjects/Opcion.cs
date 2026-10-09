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
        public string Texto { get; set; }

        // FKs (CapituloOrigenId y CapituloDestinoId) como shadow properties.
        public Intermedio CapituloOrigen { get; set; }
        public Capitulo CapituloDestino { get; set; }


    }
}
