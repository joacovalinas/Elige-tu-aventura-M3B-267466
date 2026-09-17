using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Intermedio: Capitulo
    {
        public List<Opcion> SiguienteCap { get; set; }
    }
}
