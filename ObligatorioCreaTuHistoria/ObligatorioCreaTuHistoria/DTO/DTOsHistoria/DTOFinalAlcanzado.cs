using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsHistoria
{
    public class DTOFinalAlcanzado
    {
        public int FinalId { get; set; }
        public string Final { get; set; }
        public int CantidadVeces { get; set; }
        public bool EsMasFrecuente { get; set; }
    }
}
