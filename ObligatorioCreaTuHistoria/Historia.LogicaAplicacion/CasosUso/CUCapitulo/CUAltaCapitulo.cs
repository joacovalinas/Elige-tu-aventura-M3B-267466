using DTOs.DTO.DTOsCapitulo;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCapitulo;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUCapitulo
{
    public class CUAltaCapitulo : ICUAltaCapitulo
    {

        private IRepositorioCapitulo _repoCapitulo;
        public void Ejecutar(DTOAltaCapitulo dto)
        {
            throw new NotImplementedException();
        }
    }
}
