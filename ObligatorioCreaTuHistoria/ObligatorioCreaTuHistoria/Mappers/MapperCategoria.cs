using DTOs.DTO.DTOsCategoria;
using DTOs.DTO.DTOsRol;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperCategoria
    {
        public static DTOCategoria ToDtoCategoria(Categoria c)
        {

            DTOCategoria dto = new DTOCategoria();
            dto.Nombre = c.Nombre;
            return dto;

        }

        public static List<DTOCategoria> ToListDtoCategoria(List<Categoria> l)
        {

            List<DTOCategoria> ret = new List<DTOCategoria>();

            foreach (Categoria c in l)
            {

                ret.Add(ToDtoCategoria(c));
            }

            return ret;

        }

    }
}
