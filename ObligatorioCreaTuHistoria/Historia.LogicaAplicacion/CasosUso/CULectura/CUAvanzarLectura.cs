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

        public void Ejecutar(string NombreLectura, string NombreCapitulo, string NombreOpcion)
        {
            
            Lectura l= _repositorioLectura.FindByNombreLectura(NombreLectura);
            Intermedio aux = (Intermedio)_repositorioCapitulo.FindByNombreCapitulo(NombreCapitulo);

            if (aux.Opciones.Find(x => x.Texto == NombreOpcion).CapituloDestino.GetType() == typeof(Final))
            {
                l.FechaFin = DateTime.Now;
                l.Final = (Final)aux.Opciones.Find(x => x.Texto == NombreOpcion).CapituloDestino;
                l.Final.CantidadDeVecesAlcanzado += 1;
            }
            else {
                l.CapituloActual = aux.Opciones.Find(x => x.Texto == NombreOpcion).CapituloDestino;
            }


            

            _repositorioLectura.Update(l);

        }
    }
}
