using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsAuth
{
    public class DTOLoginRespuesta
    {
        public int IdUsuario { get; set; }
        public string Token { get; set; } 
        public string Rol { get; set; } 
        public string Nombre { get; set; } 
     
    }
}
