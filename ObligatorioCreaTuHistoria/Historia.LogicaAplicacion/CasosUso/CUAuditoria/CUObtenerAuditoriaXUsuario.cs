
using DTOs.DTO.DTOsAuditoria;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUAuditoria
{
    public class CUObtenerAuditoriaXUsuario : ICUObtenerAuditoriaXUsuario
    {
        private IRepositorioAuditoria _repositorioAuditoria;
        private IRepositorioUsuario _repositorioUsuario;    
        public CUObtenerAuditoriaXUsuario(IRepositorioAuditoria repositorioAuditoria, IRepositorioUsuario repositorioUsuario)
        {
            _repositorioAuditoria = repositorioAuditoria;
            _repositorioUsuario = repositorioUsuario;

        }

        public List<DTOAuditoria> ObtenerAuditoriaXUsuario(string NombreUsuario)
        {
            int idUsuario = _repositorioUsuario.FindByNombreUsuario(NombreUsuario).Id;

            List<DTOAuditoria> auditoria = MapperAuditoria.ToListDtoAuditoria(_repositorioAuditoria.ObtenerPorUsuario(idUsuario));
            return auditoria;

        }
    }
}
