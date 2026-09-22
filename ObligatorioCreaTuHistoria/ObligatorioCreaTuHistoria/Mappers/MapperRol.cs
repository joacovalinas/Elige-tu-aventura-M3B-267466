using DTOs.DTO.DTOsRol;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperRol
    {

        public static DTORol ToDtoRol(Rol r)
        {

            DTORol dto = new DTORol();
            dto.Id = r.Id;
            dto.Nombre = r.Nombre;
            return dto;

        }

        public static List<DTORol> ToListDtoRol(List<Rol> l)
        {

            List<DTORol> ret = new List<DTORol>();

            foreach (Rol r in l)
            {

                ret.Add(ToDtoRol(r));
            }

            return ret;

        }


    }
}
