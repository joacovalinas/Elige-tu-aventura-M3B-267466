using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Final: Capitulo
    {
        public int CantidadDeVecesAlcanzado { get; set; }

        public override void Validar()
        {
            ValidarBase();

           
        }
    }
}
