using DTOs.DTO.DTOsHistoria;
using DTOs.DTO.DTOsUsuario;
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
    public class CUAltaHistoria : ICUAltaHistoria
    {
        private  IRepositorioHistoria _repoHistorias;
        private  IRepositorioCategoria _repoCategorias;

        private ICUAltaAuditoria cuAltaAuditoria;

        public CUAltaHistoria(IRepositorioHistoria historias, IRepositorioCategoria categorias, ICUAltaAuditoria AltaAudi)
        {
            _repoHistorias = historias;
            _repoCategorias = categorias;
            cuAltaAuditoria = AltaAudi;
        }




        public void Ejecutar(DTOAltaHistoria dto, int usuId)
        {
            // 1. DTO nulo
            if (dto == null)
                throw new DominioException("Los datos de la historia son obligatorios.");

            // 2. Normalizar el título
            string titulo = dto.Titulo?.Trim();

            // 3. Unicidad del título (consulta a la base)
            if (_repoHistorias.FindByNombreHistoria(titulo) != null)
                throw new HistoriaYaExisteException("Ya existe una historia con ese título.");

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
            Historia historia = new Historia
            {
                Titulo = titulo,
                Sinopsis = dto.Sinopsis?.Trim()
            };
            historia.CatHistoria.AddRange(categorias);
            historia.Validar();

            // 6. Capítulo inicial: construir y validar
            Intermedio inicial = new Intermedio
            {
                Titulo = dto.TituloCapInicial?.Trim(),
                Cuerpo = dto.CuerpoCapInicial?.Trim(),
                Historia = historia
            };
            inicial.Validar();

            // 7. Armar el grafo y persistir
            historia.CapInicial = inicial;
            historia.Capitulos.Add(inicial);


            _repoHistorias.Add(historia);

            // RF05: registrar Auditoria .


            cuAltaAuditoria.Ejecutar("AltaHistoria", historia.Id, usuId);

        }



    }
    
}
