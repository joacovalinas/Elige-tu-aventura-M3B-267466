using DTOs.DTO.DTOsCategoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsHistoria
{
    public class DTOIndexHistoria
    {
        public List<DTOHistoria> Historias { get; set; } 
        public List<DTOCategoria> Categorias { get; set; } 
        public int? CategoriaId { get; set; }
    }
}
