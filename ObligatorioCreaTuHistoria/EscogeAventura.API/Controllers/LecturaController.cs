using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaController : ControllerBase
    {
        private ICUAltaLectura _cuAltaLectura;

        public LecturaController(ICUAltaLectura cuAltaLectura)
        {
            _cuAltaLectura = cuAltaLectura;
        }

        [HttpPost("ALtaLectura")]
        public IActionResult AltaLectura(DTOAltaLectura dto)
        {
            try
            {
                _cuAltaLectura.Ejecutar(dto);
                return StatusCode(200, "Lectura registrada correctamente.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }   
               


    }
}
