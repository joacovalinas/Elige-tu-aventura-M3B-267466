using Historia.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Historia.LogicaNegocios.Entidades
{
    public class Historia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public List<Categoria> CatHistorias { get; set; } = new List<Categoria>();
        public Intermedio CapInicial { get; set; }
    }
}
