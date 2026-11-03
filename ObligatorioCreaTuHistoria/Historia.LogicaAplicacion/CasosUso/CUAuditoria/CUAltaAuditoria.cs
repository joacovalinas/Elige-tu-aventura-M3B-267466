using DTOs.DTO.DTOsHistoria;
using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using EscogeAventura.LogicaNegocios.CustomExceptions;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUAuditoria
{
    public class CUAltaAuditoria : ICUAltaAuditoria 
    {
        private IRepositorioAuditoria _repoAuditoria;
        private IRepositorioHistoria _repoHistoria;
        private IRepositorioUsuario _repoUsuario;

        public CUAltaAuditoria(IRepositorioAuditoria  repoAuditoria, IRepositorioHistoria repoHistoria, IRepositorioUsuario repoUsuario)
        {
            _repoAuditoria = repoAuditoria;
            _repoHistoria = repoHistoria;
            _repoUsuario = repoUsuario;
        }

        public  void Ejecutar(string accion, int histId, int usuId)
        {





            if (histId== null) 
            throw new AuditoriaException("Historia no puede estar vacia");
          
            if (usuId == null) 
            throw new AuditoriaException("Usuario no puede estar vacio");
            
            if (string.IsNullOrWhiteSpace(accion)) 
            throw new AuditoriaException("Accion no puede estar vacia");


            Historia verifH = _repoHistoria.FindById(histId);

            Usuario verifU = _repoUsuario.FindById(usuId);

            if (verifH == null)
                throw new AuditoriaException("No existe esa historia");
            if (verifU == null)
                throw new AuditoriaException("No existe ese usuario");


            Auditoria audi = new Auditoria();
            audi.Accion = accion;
            audi.Historia = verifH;
            audi.Usuario = verifU;

            audi.Validar();

            _repoAuditoria.Add(audi);






        }

        
    }
}
