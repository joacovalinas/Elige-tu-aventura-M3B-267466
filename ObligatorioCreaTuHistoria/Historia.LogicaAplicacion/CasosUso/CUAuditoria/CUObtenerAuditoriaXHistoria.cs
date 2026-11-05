using DTOs.DTO.DTOsAuditoria;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUAuditoria
{
    public class CUObtenerAuditoriaXHistoria : ICUObtenerAuditoriaXHistoria
    {

        private IRepositorioAuditoria _repositorioAuditoria;
        private IRepositorioHistoria _repositorioHistoria;
        public CUObtenerAuditoriaXHistoria(IRepositorioAuditoria repositorioAuditoria, IRepositorioHistoria repositorioHistoria)
        {
            _repositorioAuditoria = repositorioAuditoria;
            _repositorioHistoria = repositorioHistoria;

        }

        public List<DTOAuditoria> ObtenerAuditoriaXHistoria(string Nombrehistoria)
        {

            int idHistoria = _repositorioHistoria.FindByNombreHistoria(Nombrehistoria).Id;

            List<DTOAuditoria> auditoria = MapperAuditoria.ToListDtoAuditoria(_repositorioAuditoria.ObtenerPorHistoria(idHistoria));
            return auditoria;

        }


    }
}
