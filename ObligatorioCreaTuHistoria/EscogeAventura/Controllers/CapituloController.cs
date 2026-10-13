using DTOs.DTO.DTOsCapitulo;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCapitulo;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EscogeAventura.WebApp.Controllers
{
    public class CapituloController : Controller
    {
        private readonly ICUAltaCapitulo _agregar;
        private readonly IRepositorioHistoria _historias;

        public CapituloController(ICUAltaCapitulo agregar, IRepositorioHistoria historias)
        {
            _agregar = agregar;
            _historias = historias;
        }

        [HttpGet]
        public IActionResult Alta(int historiaId)
         {
            if (!CargarDatosFormulario(historiaId))
               return NotFound();

            DTOAltaCapitulo dto = new DTOAltaCapitulo();
            dto.HistoriaId = historiaId;
            return View(dto);
        }

        [HttpPost]
        
        public IActionResult Alta(DTOAltaCapitulo dto)
        {
            try
            {
                //int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _agregar.Ejecutar(dto);
                TempData["Mensaje"] = "Capítulo agregado.";
                return RedirectToAction("Crear", new { historiaId = dto.HistoriaId });
            }
            catch (DominioException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                if (!CargarDatosFormulario(dto.HistoriaId))
                    return NotFound();
                return View(dto);
            }
        }

        // Carga la historia y sus capitulos intermedios (los unicos que pueden ser antecedente).
        private bool CargarDatosFormulario(int historiaId)
        {
            Historia historia = _historias.ObtenerPorIdConCapitulos(historiaId);
            if (historia == null)
                return false;

            ViewBag.Historia = historia;
            ViewBag.Intermedios = historia.Capitulos.OfType<Intermedio>().ToList();
            return true;
        }
    }
}
