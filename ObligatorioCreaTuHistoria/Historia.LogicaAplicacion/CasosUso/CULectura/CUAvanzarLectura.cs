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
            
            Lectura l= _repositorioLectura.FindById(dto.Id);
            Capitulo aux = _repositorioCapitulo.FindByNombreCapituloYHistoria(dto.opcionElegida, dto.HistoriaTitulo);

            if (aux is Final final)
            {
                l.FechaFin = DateTime.Now;
                l.Final = final;
                final.CantidadDeVecesAlcanzado += 1;
                l.CapituloActual = final; // para que quede posicionada en el final
            }
            else
            {
               
                l.CapituloActual = aux;
            }

            _repositorioLectura.Update(l);

            dto.capituloActual = MapperCapitulo.ToDtoCapituloLectura(aux);
            dto.tipoCapitulo = aux.GetType().Name;
            dto.fechafin = l.FechaFin; // null si todavía no terminó
   

            return dto;
        }
    }
    }

