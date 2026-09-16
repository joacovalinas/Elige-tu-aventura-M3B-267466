using System;
using System.Collections.Generic;
using System.Text;

namespace Historia.LogicaNegocios.ValueObjects
{
    public record Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
