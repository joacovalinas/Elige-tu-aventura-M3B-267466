using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize (Roles = "Usuario")]
    public class LecturaController : ControllerBase
    {
        private ICUAltaLectura _cuAltaLectura;
        private ICUObtenerLectura _cuObtenerLectura;
        private ICUAvanzarLectura _cuAvanzarLectura;

        private ICULeerLectura _cuLeerLectura;
        public LecturaController(ICUAltaLectura cuAltaLectura, ICUObtenerLectura cuObtenerLectura, ICUAvanzarLectura cuAvanzarLectura, ICULeerLectura cuLeerLectura)
        {
            _cuAltaLectura = cuAltaLectura;
            _cuObtenerLectura = cuObtenerLectura;
            _cuAvanzarLectura = cuAvanzarLectura;
            _cuLeerLectura = cuLeerLectura;
        }




        [HttpPost("ALtaLectura")]
        public IActionResult AltaLectura(DTOAltaLectura dto)
        {
            try
            {

                 int IdLectura = _cuAltaLectura.Ejecutar(dto);

                   return Ok(new { IdLectura = IdLectura });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{IdLectura:int}")]
        public IActionResult LeerLectura(int IdLectura)
        {
            try
            {
                DTOLeer dto = _cuLeerLectura.Ejecutar(IdLectura);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("Avanzar")]
        public IActionResult AvanzarLectura(DTOLeer dto)
        {
            try
            {
                 _cuAvanzarLectura.Ejecutar(dto);
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }



        }
        }
}
