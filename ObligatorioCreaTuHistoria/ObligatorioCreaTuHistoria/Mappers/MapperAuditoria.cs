using DTOs.DTO.DTOsAuditoria;
using DTOs.DTO.DTOsCategoria;
using EscogeAventura.LogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperAuditoria
    {
        public static DTOAuditoria ToDtoAuditoria(Auditoria a)
        {

            DTOAuditoria dto = new DTOAuditoria();
            dto.Accion = a.Accion;
            dto.Usuario = a.Usuario.NomUsu;
            dto.FechaHora = a.Fecha;
            dto.HistoriaNombre = a.Historia.Titulo;
            return dto;

        }

        public static List<DTOAuditoria> ToListDtoAuditoria(List<Auditoria> l)
        {

            List<DTOAuditoria> ret = new List<DTOAuditoria>();

            foreach (Auditoria a in l)
            {

                ret.Add(ToDtoAuditoria(a));
            }

            return ret;

        }

    }
}
