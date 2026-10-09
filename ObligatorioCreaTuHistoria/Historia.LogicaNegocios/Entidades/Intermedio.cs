using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Intermedio: Capitulo
    {
        public List<Opcion> Opciones { get; set; }

        public override void Validar()
        {
            ValidarBase();

            
        }
    }
}
