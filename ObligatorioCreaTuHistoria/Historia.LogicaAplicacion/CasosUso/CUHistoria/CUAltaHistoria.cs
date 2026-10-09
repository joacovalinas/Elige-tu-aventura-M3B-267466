using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria
{
    public class CUAltaHistoria : ICUAltaHistoria
    {
        private  IRepositorioHistoria _repoHistorias;
        private  IRepositorioCategoria _repoCategorias;



        public CUAltaHistoria(IRepositorioHistoria historias, IRepositorioCategoria categorias)
        {
            _repoHistorias = historias;
            _repoCategorias = categorias;
        }




        public void Ejecutar(DTOAltaHistoria dto)
        {
            if (dto == null)
                throw new DominioException("Los datos de la historia son obligatorios.");

            // Nombres pedidos: sin vacíos, sin espacios sobrantes y sin repetidos
            List<string> nombresPedidos = (dto.CatHistoria ?? new List<string>())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Una sola consulta a la base
            List<Categoria> categorias = _repoCategorias.ObtenerPorNombres(nombresPedidos);

            // Nombres que vinieron del DTO pero no existen
            List<string> inexistentes = nombresPedidos
                .Except(categorias.Select(c => c.Nombre), StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (inexistentes.Any())
                throw new DominioException("No existen las categorías: " + string.Join(", ", inexistentes) + ".");

            Historia historia = new Historia
            {
                Titulo = dto.Titulo?.Trim(),
                Sinopsis = dto.Sinopsis?.Trim()
            };
            historia.CatHistoria.AddRange(categorias);

            historia.Validar();
            historia.AsignarCapituloInicial(dto.TituloCapInicial, dto.CuerpoCapInicial);

            _repoHistorias.Add(historia);

            // RF05: acá registrarías la Auditoria (acción "Creación", fecha, adminId).
        }
    }
    
}
