using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CULectura
{
    public class CULeerLectura : ICULeerLectura
    {
        private  IRepositorioLectura _repositorioLectura;
        private IRepositorioUsuario _repositorioUsuario;
        private IRepositorioCapitulo _repositorioCapitulo;
        public CULeerLectura(IRepositorioLectura repositorioLectura, IRepositorioUsuario repositorioUsuario, IRepositorioCapitulo repositorioCapitulo)
        {
            _repositorioLectura = repositorioLectura;
            _repositorioUsuario = repositorioUsuario;
            _repositorioCapitulo = repositorioCapitulo;

        }

        public DTOLeer Ejecutar(int IdLectura)
        {
            Lectura? lecturaAux = _repositorioLectura.FindById(IdLectura);


            DTOLeer dto = new DTOLeer();
                Capitulo cap = _repositorioCapitulo.FindById(lecturaAux.CapituloActual.Id);
                dto.Id = lecturaAux.Id;
                dto.capituloActual = MapperCapitulo.ToDtoCapituloLectura(cap);
                dto.HistoriaTitulo = lecturaAux.Historia.Titulo;
                dto.nombreLectura = lecturaAux.Nombrelectura;
                dto.tipoCapitulo = lecturaAux.CapituloActual.GetType().Name;
           
            if(dto.tipoCapitulo == "Final")
            {
                lecturaAux.finalizado = true;
                
                _repositorioLectura.Update(lecturaAux);
                dto.fechafin = lecturaAux.FechaFin;

            }
                
           
            return dto;
            

         
           
        }
    }
}
