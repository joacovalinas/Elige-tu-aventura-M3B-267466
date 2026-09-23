using EscogeAventura.LogicaNegocios.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Historia
    {
        [Key]

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public EstadoHistoria Estado { get; set; }
        public List<Categoria> CatHistorias { get; set; } = new List<Categoria>();
        public Intermedio CapInicial { get; set; }












    }
}
