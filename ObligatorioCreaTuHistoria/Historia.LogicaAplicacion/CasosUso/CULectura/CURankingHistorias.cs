using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CULectura
{
    public class CURankingHistorias : ICURankingHistorias
    {
        private  IRepositorioLectura _repositorioLectura;

        public CURankingHistorias(IRepositorioLectura repositorioLectura)
        {
            _repositorioLectura = repositorioLectura;
        }

        public List<DTORankingHistorias> Ejecutar()
        {
            return _repositorioLectura.ObtenerFinalizadas()   // debe incluir Historia
                      .GroupBy(l => new { l.Historia.Id, l.Historia.Titulo })
                      .Select(g => new DTORankingHistorias
                      {
                          Historia = g.Key.Titulo,
                          CantidadLecturas = g.Count()
                      })
                      .OrderByDescending(r => r.CantidadLecturas)
                      .ThenBy(r => r.Historia)
                      .ToList();
        }
    }
}
