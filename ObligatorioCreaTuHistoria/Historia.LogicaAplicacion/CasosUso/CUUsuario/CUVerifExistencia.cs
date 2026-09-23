using DTOs.DTO.DTOsUsuario;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUVerifExistencia : ICUVerifExistencia
    {
        private IRepositorioUsuario _repoUsuario;

        public CUVerifExistencia(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        public DTOUsuario Ejecutar(string email, string password)
        {
            Usuario buscado = _repoUsuario.FindByEmail(email);

            if (buscado is null)
            {
                return null;
            }

            bool macheaPassword = Contrasena.Verify(password, buscado.Contrasena.Pass);

            if (macheaPassword)
            {

                //si llega acá la persona puso bien los datos.
                DTOUsuario ret = MapperUsuario.ToDtoUsuario(buscado);
                return ret;
            }
            else
            {
                return null;
            }
        }
    }
}
