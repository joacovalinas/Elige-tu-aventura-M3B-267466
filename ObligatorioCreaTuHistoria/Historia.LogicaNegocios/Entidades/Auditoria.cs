using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Auditoria
    {
        public Auditoria()
        {
            Fecha = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Accion { get; set; }
        public Historia Historia { get; set; }
        public Usuario Usuario { get; set; }
        

        public void Validar()
        {
            if (Fecha == default)
                throw new DatoVacioException("La fecha de la auditoría es obligatoria.");

            if (string.IsNullOrWhiteSpace(Accion))
                throw new DatoVacioException("La acción de la auditoría es obligatoria.");

            if (Historia == null)
                throw new DatoVacioException("La historia de la auditoría es obligatoria.");

            if (Usuario == null)
                throw new DatoVacioException("El usuario de la auditoría es obligatorio.");
        }
    }

 


    
}