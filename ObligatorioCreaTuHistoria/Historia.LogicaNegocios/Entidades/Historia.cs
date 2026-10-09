using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscogeAventura.LogicaNegocios.Entidades
{
    public class Historia
    {
        public Historia()
        {
            Estado = EstadoHistoria.Borrador;
        }

        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public EstadoHistoria Estado { get; set; }
        public List<Categoria> CatHistoria { get; set; } 

        public Intermedio CapInicial { get; set; }

        public List<Capitulo> Capitulos { get; set; } = new List<Capitulo>();
      

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                throw new DatoVacioException("El título de la historia es obligatorio.");

            if (string.IsNullOrWhiteSpace(Sinopsis))
                throw new DatoVacioException("La sinopsis es obligatoria.");

            if (CatHistoria == null || CatHistoria.Count == 0)
                throw new DatoVacioException("La historia debe pertenecer al menos a una categoría.");
        }

        public void AsignarCapituloInicial(string titulo, string cuerpo)
        {
            if (CapInicial != null)
                throw new DominioException("La historia ya tiene un capítulo inicial.");

            Intermedio inicial = new Intermedio
            {
                Titulo = titulo?.Trim(),
                Cuerpo = cuerpo?.Trim(),
                Historia = this
            };


            inicial.Validar();

            CapInicial = inicial;
            Capitulos.Add(inicial);
        }


    }
}
