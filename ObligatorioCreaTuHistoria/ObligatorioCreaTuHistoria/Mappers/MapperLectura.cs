using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperLectura
    {


        public static DTOLectura ToDtoLectura(Lectura l)
        {

            DTOLectura dto = new DTOLectura();
            dto.Id = l.Id;
            dto.Nombrelectura = l.Nombrelectura;
            dto.Usuario = MapperUsuario.ToDtoUsuario(l.Usuario);
            dto.Historia = MapperHistoria.ToDtoHistoria(l.Historia);
            dto.FechaInicio = l.FechaInicio;
            dto.FechaFin = l.FechaFin;
            dto.Final = MapperCapitulo.ToDtoCapitulo(l.Final);
            dto.CapituloActual = MapperCapitulo.ToDtoCapitulo(l.CapituloActual);
            return dto;

        }

        public static List<DTOLectura> ToListDtoLectura(List<Lectura> l)
        {

            List<DTOLectura> ret = new List<DTOLectura>();

            foreach (Lectura c in l)
            {

                ret.Add(ToDtoLectura(c));
            }

            return ret;



        }
    }
    }
