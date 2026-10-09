using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public record Categoria 
    {
        [Key]

        public int Id { get; set; }
        [Required]
        
        public string Nombre { get; set; }


        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new DatoVacioException("El nombre de la categoría no puede estar vacío.");
            }
        }
    }
}
