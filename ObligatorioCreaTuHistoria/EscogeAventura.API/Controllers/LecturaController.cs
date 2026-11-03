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
    [Authorize (Roles = "Usuario")]
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
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("Lectura/{nombre}")]
        public IActionResult LeerLectura(string nombre)
        {
            try
            {
                DTOLeer dto = new DTOLeer();
                DTOLectura lectura = _cuObtenerLectura.Ejecutar(nombre);
                DTOCapitulo cap = lectura.Historia.CapInicial;

                dto.nombreLectura = nombre;
                dto.capituloActual =  lectura.CapituloActual;
                dto.HistoriaTitulo = lectura.Historia.Titulo;
                dto.capituloActual = cap;
                dto.tipoCapitulo =  cap.GetType().Name;

                return StatusCode(200,dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("Lectura/Avanzar/{nombre}")]
        public IActionResult AvanzarLectura(DTOLeer dto)
        {
            try
            {


                _cuAvanzarLectura.Ejecutar(dto);


                // Aquí puedes agregar la lógica para avanzar en la lectura, por ejemplo, cambiar el estado de la lectura o actualizar algún campo.
                return StatusCode(200,dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }



        }
        }
}
