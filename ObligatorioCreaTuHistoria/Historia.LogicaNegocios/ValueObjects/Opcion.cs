using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscogeAventura.LogicaNegocios.ValueObjects
{
    
    public class Opcion
    {
        public Opcion()
        {
        }

        [Key]
        public int Id { get; set; }
        public string Texto { get; set; }

        public Intermedio CapituloOrigen { get; set; }
        public Capitulo CapituloDestino { get; set; }


        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Texto))
                throw new DatoVacioException("El texto de la opción es obligatorio.");

            if (CapituloOrigen == null)
                throw new DominioException("La opción debe tener un capítulo de origen.");

            if (CapituloDestino == null)
                throw new DominioException("La opción debe llevar a un capítulo destino.");
        }


    }
}
