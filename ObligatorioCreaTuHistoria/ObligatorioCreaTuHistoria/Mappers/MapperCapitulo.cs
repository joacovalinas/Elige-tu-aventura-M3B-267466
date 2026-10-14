using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsCategoria;
using EscogeAventura.LogicaNegocios.Entidades;
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
