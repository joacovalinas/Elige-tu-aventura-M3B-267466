using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriaController : ControllerBase
    {
        private ICUListarHistoria _cuListarHistoria;
        private ICUObtenerCategorias _cuObtenerCategorias;

        public HistoriaController(ICUListarHistoria cuListarHistoria, ICUObtenerCategorias cuObtenerCategorias)
        {
            _cuListarHistoria = cuListarHistoria;
            _cuObtenerCategorias = cuObtenerCategorias;
        }


        [HttpGet("listar")]
        public IActionResult ListarHistorias()
        {
            try
            {
                DTOIndexHistoria dto = new DTOIndexHistoria();
                dto.historias = _cuListarHistoria.Ejecutar();
                dto.categorias = _cuObtenerCategorias.Ejecutar();
                return StatusCode(200, dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }








    }
}
