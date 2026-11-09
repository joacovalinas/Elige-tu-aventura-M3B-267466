using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CURankingUsuarios : ICURankingUsuarios
    {
        private IRepositorioLectura _repositorioLectura;

        public CURankingUsuarios(IRepositorioLectura repositorioLectura)
        {
            _repositorioLectura = repositorioLectura;
        }
        public List<DTORankingUsuarios> Ejecutar()
        {
            List<Lectura> lecturas = _repositorioLectura.ObtenerFinalizadas(); 

            List<DTORankingUsuarios> ranking = lecturas
                .GroupBy(l => l.Usuario.Id)
                .Select(g => new DTORankingUsuarios
                {
                    Usuario = g.First().Usuario.NomUsu,
                    CantidadHistorias = g.Select(l => l.Historia.Id).Distinct().Count()
                })
                .OrderByDescending(r => r.CantidadHistorias)
                .ThenBy(r => r.Usuario)
                .ToList();

            return ranking;
        }
    }
    
    
}
