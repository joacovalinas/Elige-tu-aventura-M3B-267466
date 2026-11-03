using DTOs.DTO.DTOsAuditoria;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUAuditoria
{
    public class CUListarAuditoria : ICUListarAuditoria
    {
        private  IRepositorioAuditoria _repositorioAuditoria;
        public CUListarAuditoria(IRepositorioAuditoria repositorioAuditoria)
        {
            _repositorioAuditoria = repositorioAuditoria;
        }
        public List<DTOAuditoria> Ejecutar()
        {
            return MapperAuditoria.ToListDtoAuditoria(_repositorioAuditoria.FindAll());

        }
    }
}
