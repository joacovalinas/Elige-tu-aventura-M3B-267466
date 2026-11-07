using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria
{
    public class CUFinalAlcanzado : ICUFinalAlcanzado
    {
        private  IRepositorioLectura _repositorioLectura;
        private IRepositorioHistoria _repositorioHistoria;

        public CUFinalAlcanzado(IRepositorioLectura repositorioLectura, IRepositorioHistoria repositorioHistoria)
        {
            _repositorioLectura = repositorioLectura;
            _repositorioHistoria = repositorioHistoria;
        }
        public List<DTOFinalAlcanzado> Ejecutar(int historiaId)
        {
            Historia historia = _repositorioHistoria.FindById(historiaId); // debe incluir Capitulos
            if (historia == null)
                throw new Exception("La historia no existe");

            List<Lectura> lecturas = _repositorioLectura.ObtenerFinalizadasPorHistoria(historiaId); // debe incluir Final

            List<DTOFinalAlcanzado> resultado = historia.Capitulos
                .OfType<Final>()
                .Select(f => new DTOFinalAlcanzado
                {
                    FinalId = f.Id,
                    Final = f.Titulo,
                    CantidadVeces = lecturas.Count(l => l.Final != null && l.Final.Id == f.Id)
                })
                .OrderByDescending(r => r.CantidadVeces)
                .ThenBy(r => r.Final)
                .ToList();

            int maximo = resultado.Count > 0 ? resultado.Max(r => r.CantidadVeces) : 0;

            if (maximo > 0)
            {
                resultado
                    .Where(r => r.CantidadVeces == maximo)
                    .ToList()
                    .ForEach(r => r.EsMasFrecuente = true);
            }

            return resultado;




        }
    }
}
