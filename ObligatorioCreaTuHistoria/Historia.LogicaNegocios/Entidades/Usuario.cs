using Historia.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Historia.LogicaNegocios.Entidades
{
    public class Usuario
    {
        // Constructor parameterless para EF
        public Usuario() { }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public NombreCompleto NombreCompleto { get; set; } 

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } 

        [Required]
        [MaxLength(100)]
        public string NomUsu { get; set; } 

        [Required]
        public Contrasena Contrasena { get; set; }

        [Required]
        public Rol Rol { get; set; } 

        public virtual List<Lectura> Biblioteca { get; set; } = new List<Lectura>();
    }
}
