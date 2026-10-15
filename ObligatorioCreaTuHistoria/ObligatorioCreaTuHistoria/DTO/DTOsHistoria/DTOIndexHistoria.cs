using DTOs.DTO.DTOsCategoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsHistoria
{
    public class DTOIndexHistoria
    {
        public List<DTOHistoria> historias { get; set; }
        public List<DTOCategoria> categorias { get; set; }

    }
}
