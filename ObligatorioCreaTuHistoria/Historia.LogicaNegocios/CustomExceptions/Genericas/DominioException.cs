using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.CustomExceptions.Genericas
{
    public class DominioException : Exception
    {
        public DominioException(string mensaje) : base(mensaje)
        {
        }
    }
}
