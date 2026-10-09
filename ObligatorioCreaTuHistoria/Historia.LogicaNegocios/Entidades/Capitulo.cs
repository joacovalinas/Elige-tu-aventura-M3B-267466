using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public abstract class Capitulo
    {
        public Capitulo()
        {
        }

        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }

        // La FK (HistoriaId) es shadow property: se configura en OnModelCreating.
        public Historia Historia { get; set; }

        // Cada subclase (Intermedio, Final) implementa su propia validacion.
        public abstract void Validar();


        protected void ValidarBase()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                throw new DatoVacioException("El título del capítulo es obligatorio.");

            if (string.IsNullOrWhiteSpace(Cuerpo))
                throw new DatoVacioException("El texto del capítulo es obligatorio.");
        }

    }
}
