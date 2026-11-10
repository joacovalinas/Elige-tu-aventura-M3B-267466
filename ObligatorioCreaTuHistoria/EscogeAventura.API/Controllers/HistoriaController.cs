    using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoriaController : ControllerBase
    {
        private ICUListarHistoriasPublicadas _cuListarHistoriasPublicadas;
        private ICUObtenerCategorias _cuObtenerCategorias;

        public HistoriaController(ICUListarHistoriasPublicadas cuListarHistoriasPublicadas, ICUObtenerCategorias cuObtenerCategorias)
        {
            _cuListarHistoriasPublicadas = cuListarHistoriasPublicadas;
            _cuObtenerCategorias = cuObtenerCategorias;
        }

        
        [HttpGet("listar")]
        [Authorize(Roles = "Usuario,Admin")]
        public IActionResult ListarHistorias( int? categoriaId)
        {
            try
            {
                DTOIndexHistoria dto = new DTOIndexHistoria();
                dto.Historias = _cuListarHistoriasPublicadas.Ejecutar(categoriaId);
                dto.Categorias = _cuObtenerCategorias.Ejecutar();
                return StatusCode(200, dto);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}
