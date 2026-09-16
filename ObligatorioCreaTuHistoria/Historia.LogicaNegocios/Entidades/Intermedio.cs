using Historia.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Historia.LogicaNegocios.Entidades
{
    public class Intermedio: Capitulo
    {
        public List<Opcion> SiguienteCap { get; set; }
    }
}
