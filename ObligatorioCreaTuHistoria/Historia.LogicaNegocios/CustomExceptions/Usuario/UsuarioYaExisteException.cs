using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.CustomExceptions.Usuario
{
    public class UsuarioYaExisteException : Exception
    {
        public UsuarioYaExisteException(string mensaje) : base(mensaje)
        {

        }
    }
}
