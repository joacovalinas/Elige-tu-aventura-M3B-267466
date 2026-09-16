using System;
using System.Collections.Generic;
using System.Text;

namespace Historia.LogicaNegocios.Entidades
{
    public abstract class Capitulo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }


    }
}
