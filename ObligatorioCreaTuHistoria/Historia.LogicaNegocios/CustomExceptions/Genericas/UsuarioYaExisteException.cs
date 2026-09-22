using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.CustomExceptions.Genericas
{
    public class UsuarioYaExisteException : Exception
    {
        public UsuarioYaExisteException(string mensaje) : base(mensaje)
        {

        }
    }
}
