using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsCategoria;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperCapitulo
    {

        public static DTOCapitulo ToDtoCapitulo(Capitulo c)
        {

            DTOCapitulo dto = new DTOCapitulo();
            dto.Id = c.Id;
            dto.Titulo = c.Titulo;
            dto.Cuerpo = c.Cuerpo;
            dto.HistoriaId = c.Historia.Id;
            dto.opciones = new List<string>();
            if(c.GetType() == typeof(Intermedio))
            {
                Intermedio co = (Intermedio)c;
                foreach (Opcion o in co.Opciones)
                {
                    dto.opciones.Add(o.Texto);
                }
            }

            return dto;

        }


        public static DTOCapituloLectura ToDtoCapituloLectura(Capitulo c)
        {

            DTOCapituloLectura dto = new DTOCapituloLectura();
            dto.Id = c.Id;
            dto.Titulo = c.Titulo;
            dto.Cuerpo = c.Cuerpo;
            dto.opciones = new List<string>();
            if (c.GetType() == typeof(Intermedio))
            {
                Intermedio co = (Intermedio)c;
                foreach (Opcion o in co.Opciones)
                {
                    dto.opciones.Add(o.Texto);
                }
            }

            return dto;

        }


        public static List<DTOCapitulo> ToListDtoCapitulo(List<Capitulo> l)
        {

            List<DTOCapitulo> ret = new List<DTOCapitulo>();

            foreach (Capitulo c in l)
            {

                ret.Add(ToDtoCapitulo(c));
            }

            return ret;

        }

    }
}
