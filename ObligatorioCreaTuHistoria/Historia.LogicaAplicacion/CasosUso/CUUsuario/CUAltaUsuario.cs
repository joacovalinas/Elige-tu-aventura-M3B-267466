using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioRol _repoRol;

        public CUAltaUsuario(IRepositorioUsuario repoUsuario, IRepositorioRol repoRol)
        {
            _repoUsuario = repoUsuario;
            _repoRol = repoRol;

        }
        public void Ejecutar(DTOAltaUsuario dto)
        {
            Usuario nuevoUsu = new Usuario();
            nuevoUsu.NombreCompleto = new NombreCompleto(dto.Nombre, dto.Apellido);
            nuevoUsu.Email= dto.Email;
            nuevoUsu.Contrasena= new Contrasena(dto.Contrasena);
            nuevoUsu.NomUsu = dto.NomUsu;

            Rol r = _repoRol.FindByNombreRol(dto.Rol);

            nuevoUsu.Rol = r;

        }
    }
}
