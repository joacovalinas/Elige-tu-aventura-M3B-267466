using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CULectura
{
    public class CUAvanzarLectura : ICUAvanzarLectura
    {
        private IRepositorioLectura _repositorioLectura;
        private IRepositorioCapitulo _repositorioCapitulo;
        public CUAvanzarLectura(IRepositorioLectura repositorioLectura, IRepositorioCapitulo repositorioCapitulo )
        {
            _repositorioLectura = repositorioLectura;
            _repositorioCapitulo = repositorioCapitulo;
        }

        public DTOLeer Ejecutar(DTOLeer dto)
        {
            
            Lectura l= _repositorioLectura.FindByNombreLectura(dto.nombreLectura);
            Intermedio aux = (Intermedio)_repositorioCapitulo.FindByNombreCapitulo(dto.capituloActual.Titulo);


            Capitulo destino = aux.Opciones.Find(x => x.Id == dto.opcionElegida).CapituloDestino;

            if (destino is Final final)
            {
                l.FechaFin = DateTime.Now;
                l.Final = final;
                final.CantidadDeVecesAlcanzado += 1;
                l.CapituloActual = final; // para que quede posicionada en el final
            }
            else
            {
                l.CapituloActual = destino;
            }

            _repositorioLectura.Update(l);

            dto.capituloActual = MapperCapitulo.ToDtoCapitulo(l.CapituloActual);
            dto.tipoCapitulo = l.CapituloActual.GetType().Name;
            dto.fechafin = l.FechaFin; // null si todavía no terminó
  

            return dto;
        }
    }
    }

