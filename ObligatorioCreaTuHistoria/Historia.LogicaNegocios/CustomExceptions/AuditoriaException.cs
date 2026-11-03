using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.CustomExceptions
{
    public class AuditoriaException : Exception
    {
        public AuditoriaException(string mensaje) : base(mensaje)
        {
        }
    }
}
