using DTOs.DTO.DTOsRol;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsUsuario
{
    public class DTOUsuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DTORol Rol { get; set; }

        // public bool Activa { get; set; }


    }
}
