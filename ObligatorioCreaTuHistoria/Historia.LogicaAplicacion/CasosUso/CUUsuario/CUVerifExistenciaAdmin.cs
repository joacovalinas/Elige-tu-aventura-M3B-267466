using DTOs.DTO.DTOsUsuario;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUVerifExistenciaAdmin : ICUVerifExistenciaAdmin
    {
        private IRepositorioUsuario _repoUsuario;

        public CUVerifExistenciaAdmin(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        public DTOUsuario Ejecutar(string email, string password)
        {

            Usuario buscado = _repoUsuario.FindByEmail(email);

              
              if (buscado.Rol.Nombre != "Admin")
              {
                  throw new DominioException("Solo administradores pueden hacer login aca");
              }
              
            if (buscado is null)
            {
                throw new DatoVacioException("Usuario no encontrado");
            }
            if (password is null)
            {
                throw new DatoVacioException("Contrasenia Vacia");
            }
            bool macheaPassword = Contrasena.Verify(password, buscado.Contrasena.Pass);

            if (macheaPassword)
            {

                DTOUsuario ret = MapperUsuario.ToDtoUsuario(buscado);
                return ret;
            }
            else
            {
               throw new DatoVacioException("Contrasenia incorrecta");
            }
        }
    }
}
