using DTOs.DTO.DTOsCapitulo;
using DTOs.DTO.DTOsLectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaNegocios.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaController : ControllerBase
    {
        private ICUAltaLectura _cuAltaLectura;
        private ICUObtenerLectura _cuObtenerLectura;
        private ICUAvanzarLectura _cuAvanzarLectura;
        public LecturaController(ICUAltaLectura cuAltaLectura, ICUObtenerLectura cuObtenerLectura, ICUAvanzarLectura cuAvanzarLectura)
        {
            _cuAltaLectura = cuAltaLectura;
            _cuObtenerLectura = cuObtenerLectura;
            _cuAvanzarLectura = cuAvanzarLectura;
        }

        [HttpPost("ALtaLectura")]
        public IActionResult AltaLectura(DTOAltaLectura dto)
        {
            try
            {
                _cuAltaLectura.Ejecutar(dto);
                

                return RedirectToAction("LeerLectura", new { nombre = dto.Nombrelectura });
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("Lectura/{nombre}")]
        public IActionResult LeerLectura(string nombre)
        {
            try
            {
                DTOLectura lectura = _cuObtenerLectura.Ejecutar(nombre);
                return StatusCode(200, lectura);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        [HttpPost("Lectura/Avanzar/{nombre}")]
        public IActionResult AvanzarLectura(string nombreCap, string nombreLectura, string nombreOpcion)
        {
            try
            {
                _cuAvanzarLectura.Ejecutar(nombreLectura, nombreCap, nombreOpcion);


                // Aquí puedes agregar la lógica para avanzar en la lectura, por ejemplo, cambiar el estado de la lectura o actualizar algún campo.
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.");
            }



        }
        }
}
