using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria
{
    public class CUBajaHistoria : ICUBajaHistoria
    {
        private IRepositorioHistoria _repositorioHistoria;
        private IRepositorioLectura _repositorioLectura;

        public CUBajaHistoria(IRepositorioHistoria repositorioHistoria, IRepositorioLectura repositorioLectura)
        {
            _repositorioHistoria = repositorioHistoria;
            _repositorioLectura = repositorioLectura;
        }
        public void Ejecutar(int idHistoria)
        {
            Historia hist = _repositorioHistoria.FindById(idHistoria);

            if(_repositorioLectura.FindAll().Exists(l => l.Historia.Id == idHistoria))
            {
                throw new DominioException("No se puede eliminar la historia porque tiene lecturas asociadas.");
            }


            _repositorioHistoria.Remove(hist);
        }
    }
}
