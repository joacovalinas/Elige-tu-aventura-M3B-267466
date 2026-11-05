using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CULectura
{
    public class CUAltaLectura : ICUAltaLectura
    {
        private  IRepositorioLectura _repositorioLectura;
        private IRepositorioUsuario _repositorioUsuario;
        private IRepositorioHistoria _repositorioHistoria;
        public CUAltaLectura(IRepositorioLectura repositorioLectura, IRepositorioUsuario repositorioUsuario, IRepositorioHistoria repositorioHistoria)
        {
            _repositorioLectura = repositorioLectura;
            _repositorioUsuario = repositorioUsuario;
            _repositorioHistoria = repositorioHistoria;
        }

        public int Ejecutar(DTOAltaLectura dto)
        {


            Usuario usu = _repositorioUsuario.FindById(dto.UsuarioId);
            if (usu == null)
            {
                throw new DominioException("No existe este usuario.");
            }


            Historia hist = _repositorioHistoria.FindById(dto.HistoriaId);
            if (hist == null)
            {
                throw new DominioException("No existe esta historia.");
            }

            Lectura aux = _repositorioLectura.FindAll().FirstOrDefault(l => l.Usuario.Id == dto.UsuarioId && l.Nombrelectura == dto.Nombrelectura);

            if (aux == null)
            {
                Lectura lectura = new Lectura();
                lectura.Usuario = usu;
                lectura.Historia = hist;
                lectura.Nombrelectura = dto.Nombrelectura;
                lectura.FechaInicio = DateTime.Now;
                lectura.CapituloActual = hist.CapInicial;
                lectura.Validar();

                _repositorioLectura.Add(lectura);
                return lectura.Id;
            }

            return aux.Id;

        }
    }
}
