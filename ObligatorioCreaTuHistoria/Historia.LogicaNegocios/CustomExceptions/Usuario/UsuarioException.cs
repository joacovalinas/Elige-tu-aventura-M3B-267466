using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.CustomExceptions.Usuario
{
    public class UsuarioException : Exception
    {
        public UsuarioException(string message) : base(message)
        {
        }
    }
}
