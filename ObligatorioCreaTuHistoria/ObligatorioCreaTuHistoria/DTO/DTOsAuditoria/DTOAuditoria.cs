using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTO.DTOsAuditoria
{
    public class DTOAuditoria
    {

        public string Accion { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string HistoriaNombre { get; set; }
    }
}
