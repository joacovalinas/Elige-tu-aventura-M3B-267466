using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsCapitulo
{
    public  class DTOCapituloLectura
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }

        public List<string>? opciones { get; set; }

    }
}
