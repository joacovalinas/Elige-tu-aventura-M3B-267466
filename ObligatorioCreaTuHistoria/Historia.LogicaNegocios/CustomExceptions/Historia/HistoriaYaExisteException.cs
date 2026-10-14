using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.CustomExceptions.Historia
{
    public class HistoriaYaExisteException : Exception
    {
        public HistoriaYaExisteException(string mensaje) : base(mensaje)
        {

        }
    }
}
