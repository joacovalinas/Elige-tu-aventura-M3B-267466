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
        private ICUListarHistoriasPublicadas _cuListarHistoriasPublicadas;
        private ICUObtenerCategorias _cuObtenerCategorias;

        public HistoriaController(ICUListarHistoriasPublicadas cuListarHistoriasPublicadas, ICUObtenerCategorias cuObtenerCategorias)
        {
            _cuListarHistoriasPublicadas = cuListarHistoriasPublicadas;
            _cuObtenerCategorias = cuObtenerCategorias;
        }


        [HttpGet("listar")]
        public IActionResult ListarHistorias()
          {
            try
            {
                DTOIndexHistoria dto = new DTOIndexHistoria();
                dto.historias = _cuListarHistoriasPublicadas.Ejecutar();
                dto.categorias = _cuObtenerCategorias.Ejecutar();
                return StatusCode(200, dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}
