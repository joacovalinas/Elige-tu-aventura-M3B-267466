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
        public string Entidad { get; set; }
        public Historia Historia { get; set; }
        public Usuario Usuario { get; set; }
        public string Detalle { get; set; }


        public void Validar()
        {
            if (Fecha == default)
                throw new DatoVacioException("La fecha de la auditoría es obligatoria.");

            if (string.IsNullOrWhiteSpace(Accion))
                throw new DatoVacioException("La acción de la auditoría es obligatoria.");

            if (Accion != AccionesAuditoria.Creacion &&
                Accion != AccionesAuditoria.Edicion &&
                Accion != AccionesAuditoria.Publicacion)
                throw new DominioException("La acción de la auditoría no es válida.");

            if (string.IsNullOrWhiteSpace(Entidad))
                throw new DatoVacioException("La entidad de la auditoría es obligatoria.");

            if (Historia == null)
                throw new DatoVacioException("La historia de la auditoría es obligatoria.");

            if (Usuario == null)
                throw new DatoVacioException("El usuario de la auditoría es obligatorio.");
        }
    }

    public static class AccionesAuditoria
    {
        public const string Creacion = "Creacion";
        public const string Edicion = "Edicion";
        public const string Publicacion = "Publicacion";
    }
}