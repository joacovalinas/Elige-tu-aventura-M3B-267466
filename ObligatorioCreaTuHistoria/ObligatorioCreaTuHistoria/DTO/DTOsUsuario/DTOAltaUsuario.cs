using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs.DTO.DTOsUsuario
{
    public class DTOAltaUsuario
    {

        public string Nombre{ get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string NomUsu { get; set; }

        public string Contrasena { get; set; }

        public string Rol { get; set; }




    }
}
