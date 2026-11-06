using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
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
        private ICUObtenerHistorial _cuObtenerHistorial;
        public LecturaController(ICUAltaLectura cuAltaLectura, ICUObtenerLectura cuObtenerLectura, ICUAvanzarLectura cuAvanzarLectura, 
            ICULeerLectura cuLeerLectura, ICUObtenerHistorial cuObtenerHistorial)
        {
            _cuAltaLectura = cuAltaLectura;
            _cuObtenerLectura = cuObtenerLectura;
            _cuAvanzarLectura = cuAvanzarLectura;
            _cuLeerLectura = cuLeerLectura;
            _cuObtenerHistorial = cuObtenerHistorial;
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



        [HttpGet("historial/{usuarioId:int}")]
        public IActionResult ObtenerHistorial(int usuarioId, [FromQuery] DateTime? fecha)
        {
            try
            {
                List<DTOLecturaHistorial> lecturas = _cuObtenerHistorial.Ejecutar(usuarioId, fecha);
                return Ok(lecturas);
            }
            catch (UsuarioException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (DominioException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error inesperado" });
            }
        }




    }
}
