using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Historia;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria
{
    public class CUEditarHistoria : ICUEditarHistoria
    {
        private IRepositorioHistoria _repoHistorias;
        private IRepositorioCategoria _repoCategorias;

        private IRepositorioCapitulo _repoCapitulos;

        private ICUAltaAuditoria cuAltaAuditoria;
        public CUEditarHistoria(IRepositorioHistoria historias, IRepositorioCategoria categorias, IRepositorioCapitulo capitulos, ICUAltaAuditoria AltaAudi)
        {
            _repoHistorias = historias;
            _repoCategorias = categorias;
            _repoCapitulos = capitulos;
            cuAltaAuditoria = AltaAudi;

        }




        public void Ejecutar(DTOHistoria dto, int usuId)
        {
            // 1. DTO nulo
            if (dto == null)
                throw new DominioException("Los datos de la historia son obligatorios.");

            Historia historia = _repoHistorias.FindById(dto.Id);

            if (historia == null)
                throw new DominioException("La historia que se quiere editar no existe.");


            // 2. Normalizar el título
            string titulo = dto.Titulo?.Trim();

            

            // 4. Categorías: normalizar, consultar y verificar existencia
            List<string> nombresPedidos = (dto.CatHistoria ?? new List<string>())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            List<Categoria> categorias = _repoCategorias.ObtenerPorNombres(nombresPedidos);

            List<string> inexistentes = nombresPedidos
                .Except(categorias.Select(c => c.Nombre), StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (inexistentes.Any())
                throw new DominioException("No existen las categorías: " + string.Join(", ", inexistentes) + ".");

            // 5. Construir la historia CON sus categorías y recién ahí validar
            historia.Titulo = titulo;
            historia.Sinopsis = dto.Sinopsis?.Trim();
            historia.CatHistoria.Clear();
            historia.CatHistoria.AddRange(categorias);
            historia.Validar();



            if(historia.CapInicial.Cuerpo.Trim() != dto.CapInicial.Cuerpo?.Trim()
                || historia.CapInicial.Titulo.Trim() != dto.CapInicial.Titulo?.Trim())
            {

                if (historia.CapInicial.Opciones.Count != 0)
                    throw new DominioException("No se puede editar la historia porque El primer capitulo tiene capitulos  asociados.");

                Intermedio inicial = _repoCapitulos.FindById(historia.CapInicial.Id) as Intermedio;

                inicial.Titulo = dto.CapInicial.Titulo?.Trim();
                inicial.Cuerpo = dto.CapInicial.Cuerpo?.Trim();
                inicial.Historia = historia;

                inicial.Validar();
                historia.CapInicial = inicial;

            }

            

            _repoHistorias.Update(historia);

            cuAltaAuditoria.Ejecutar("Editar Historia", historia.Id, usuId);
        }
    }
}
