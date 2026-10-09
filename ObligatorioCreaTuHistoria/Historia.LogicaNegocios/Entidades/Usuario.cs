using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Usuario
    {
        
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

        public List<Lectura> Biblioteca { get; set; } 




        public Usuario() 
        {
            Biblioteca = new List<Lectura>();
        }

        public Usuario(string nombre, string apellido, string email, string nomUsu, string contrasena, Rol rol)
        {
            NombreCompleto = new NombreCompleto(nombre, apellido);
            Email = email;
            NomUsu = nomUsu;
            Contrasena = new Contrasena(contrasena);
            Rol = rol;
            Biblioteca = new List<Lectura>();
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new DatoVacioException("El correo electrónico no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(NomUsu))
            {
                throw new DatoVacioException("El nombre de usuario no puede estar vacío.");
            }
        }
    }
}
