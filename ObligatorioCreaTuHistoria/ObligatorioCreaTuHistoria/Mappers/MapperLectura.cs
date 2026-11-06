using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.ValueObjects;
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


        
        public static DTOOpcionLectura ToDtoOpcionLectura(Opcion p)
        {

            DTOOpcionLectura dto = new DTOOpcionLectura();
            dto.Id =p.Id;
            dto.Texto = p.Texto;
            return dto;

        }


        public static DTOLeer ToDtoLeer(Lectura l)
        {

            DTOLeer dto = new DTOLeer();
            dto.Id = l.Id;
            dto.nombreLectura = l.Nombrelectura;
            dto.HistoriaTitulo = l.Historia.Titulo;
            dto.capituloActual = MapperCapitulo.ToDtoCapituloLectura(l.CapituloActual);
            dto.tipoCapitulo = l.CapituloActual.GetType().Name;
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



        public static DTOLecturaHistorial ToDtoLecturaHistorial(Lectura l)
        {
            DTOLecturaHistorial dto = new DTOLecturaHistorial();
            dto.Id = l.Id;
            dto.Nombrelectura = l.Nombrelectura;
            dto.NomUsuario = l.Usuario.NomUsu;
            dto.NomHistoria = l.Historia.Titulo;
            dto.FechaInicio = l.FechaInicio;
            dto.FechaFin = l.FechaFin;
            dto.Final = l.Final?.Titulo; // Convertir a string si no es null
            dto.CapituloActual = l.CapituloActual?.Titulo; // Convertir a string si no es null
            return dto;
        }

        public static List<DTOLecturaHistorial> ToListDtoLecturaHistorial(List<Lectura> l)
        {
            List<DTOLecturaHistorial> ret = new List<DTOLecturaHistorial>();
            foreach (Lectura c in l)
            {
                ret.Add(ToDtoLecturaHistorial(c));
            }
            return ret;
        }



    }
    }
