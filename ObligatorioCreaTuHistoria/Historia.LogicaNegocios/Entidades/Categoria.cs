using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public record Categoria
    {
        [Key]

        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
