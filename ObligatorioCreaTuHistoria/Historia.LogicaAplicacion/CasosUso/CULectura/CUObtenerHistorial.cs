using DTOs.DTO.DTOsLectura;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CULectura
{
    public class CUObtenerHistorial : ICUObtenerHistorial
    {
        private IRepositorioLectura _repositorioLectura;
        private IRepositorioUsuario _repositorioUsuario;
        public CUObtenerHistorial(IRepositorioLectura repositorioLectura, IRepositorioUsuario repositorioUsuario)
        {
            _repositorioLectura = repositorioLectura;
            _repositorioUsuario = repositorioUsuario;
        }
        public List<DTOLectura> Ejecutar(int usuarioId, DateTime? fecha)
        {
           if( _repositorioUsuario.FindById(usuarioId) == null)
            {
                throw new UsuarioException("El usuario no existe");
            }
           if (fecha != null && fecha > DateTime.Now)
            {
                throw new DominioException("La fecha no puede ser mayor a la fecha actual");
            }
                List<DTOLectura> lecturas = MapperLectura.ToListDtoLectura(_repositorioLectura.Obtenerhistorial(usuarioId, fecha));
               
                return lecturas;
         }


     }
  }

