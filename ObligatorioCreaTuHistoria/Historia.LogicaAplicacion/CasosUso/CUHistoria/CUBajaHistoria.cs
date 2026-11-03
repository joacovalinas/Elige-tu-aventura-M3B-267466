using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
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
        private ICUAltaAuditoria cuAltaAuditoria;

        public CUBajaHistoria(IRepositorioHistoria repositorioHistoria, IRepositorioLectura repositorioLectura, ICUAltaAuditoria AltaAudi)
        {
            _repositorioHistoria = repositorioHistoria;
            _repositorioLectura = repositorioLectura;
            cuAltaAuditoria = AltaAudi;


        }
        public void Ejecutar(int idHistoria, int usuId)
        {
            Historia hist = _repositorioHistoria.FindById(idHistoria);

            if(_repositorioLectura.FindAll().Exists(l => l.Historia.Id == idHistoria))
            {
                throw new DominioException("No se puede eliminar la historia porque tiene lecturas asociadas.");
            }


            _repositorioHistoria.Remove(hist);
            cuAltaAuditoria.Ejecutar("Baja Historia", hist.Id, usuId);
        }
    }
}
