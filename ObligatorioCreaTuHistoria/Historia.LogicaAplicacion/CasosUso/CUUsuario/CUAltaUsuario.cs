using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
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
            try
            {
                Usuario verif = _repoUsuario.FindByEmail(dto.Email);
                if (verif != null)
                {
                    throw new UsuarioYaExisteException("Ya existe este usuario.");
                }


                Usuario nuevoUsu = new Usuario();
                nuevoUsu.NombreCompleto = new NombreCompleto(dto.Nombre, dto.Apellido);
                nuevoUsu.Email = dto.Email;
                nuevoUsu.Contrasena = new Contrasena(dto.Contrasena);
                nuevoUsu.NomUsu = dto.NomUsu;

                Rol r = _repoRol.FindById(dto.Rol);

                

                if (r is null)
                {
                    throw new DatoVacioException("El rol está nulo");
                }
                nuevoUsu.Rol = r;

                nuevoUsu.Validar();
                _repoUsuario.Add(nuevoUsu);


            }
            catch (UsuarioYaExisteException ex)
            {
                //auditoria
                throw new UsuarioYaExisteException("Ya existe este usuario.");
            }
            catch (DatoVacioException ex)
            {
                //auditoria
            }
            catch (Exception ex)
            {
                //auditoria
            }


        }
    }
}
