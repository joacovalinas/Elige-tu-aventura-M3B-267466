using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsCategoria;
using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperHistoria
    {

        public static DTOHistoria ToDtoHistoria(Historia h)
        {

            DTOHistoria dto = new DTOHistoria();
            dto.Id = h.Id;
            dto.Titulo = h.Titulo;
            dto.Sinopsis = h.Sinopsis;
            dto.Estado =h.Estado.ToString();



            foreach (Categoria c in h.CatHistoria)
            {
                if(dto.CatHistoria== null)
                {
                    dto.CatHistoria = new List<string>();
                    dto.CatHistoria.Add(c.Nombre);
                }
                else
                {
                    dto.CatHistoria.Add(c.Nombre);
                }
               
            }
            

            dto.CapInicial = MapperCapitulo.ToDtoCapitulo(h.CapInicial);

            foreach (Capitulo c in h.Capitulos)
            {

                if (dto.Capitulos == null)
                {
                    dto.Capitulos = new List<DTOCapitulo>();
                    
                    dto.Capitulos.Add(MapperCapitulo.ToDtoCapitulo(c));
                }
                else
                {
                    dto.Capitulos.Add(MapperCapitulo.ToDtoCapitulo(c));
                }
               
            }

            return dto;

        }

        public static List<DTOHistoria> ToListDtoHistoria(List<Historia> l)
        {

            List<DTOHistoria> ret = new List<DTOHistoria>();

            foreach (Historia h in l)
            {

                ret.Add(ToDtoHistoria(h));
            }

            return ret;

        }


    }
}
