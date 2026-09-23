using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class MapperUsuario
    {
        public static DTOUsuario ToDtoUsuario(Usuario u)
        {
            DTOUsuario dto = new DTOUsuario();
            dto.Id = u.Id;
            dto.Nombre = u.NombreCompleto.Nombre;
            dto.Apellido = u.NombreCompleto.Apellido;
            dto.Email = u.Email;
            dto.Rol = MapperRol.ToDtoRol(u.Rol);
            return dto;
        }
    }
}
