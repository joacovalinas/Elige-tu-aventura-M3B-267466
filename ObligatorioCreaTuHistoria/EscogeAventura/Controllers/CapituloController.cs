using DTOs.DTO.DTOsCapitulo;
using DTOs.Mappers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCapitulo;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EscogeAventura.WebApp.Controllers
{
    public class CapituloController : Controller
    {
        private readonly ICUAltaCapitulo _CuAltaCapitulo;
        private readonly IRepositorioHistoria _historias;

        public CapituloController(ICUAltaCapitulo cuAltaCapitulo, IRepositorioHistoria historias)
        {
            _CuAltaCapitulo = cuAltaCapitulo;
            _historias = historias;
        }

        [HttpGet]
        public IActionResult AltaCapitulo(int historiaId)
         {
            if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
                return RedirectToAction("Login", "Auth");

            if (_historias.FindById(historiaId) == null)
               return NotFound();

            AltaCapituloViewModel vm = CargarDatosFormulario(historiaId);

            return View(vm);
        }

        [HttpPost]
        
        public IActionResult AltaCapitulo(DTOAltaCapitulo dto)
        {
            if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
               return RedirectToAction("Login", "Auth");
            try
            {
                //int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _CuAltaCapitulo.Ejecutar(dto);
                TempData["Exito"] = "Capítulo agregado.";
               

                return RedirectToAction("AltaCapitulo", new { historiaId = dto.HistoriaId });
            } 
            catch (DominioException e)
            {
                TempData["Error"] = e.Message;
                AltaCapituloViewModel vm = CargarDatosFormulario(dto.HistoriaId);
                return View(vm);
            }
            catch (DatoVacioException e)
            {
                TempData["Error"] = e.Message;
                AltaCapituloViewModel vm = CargarDatosFormulario(dto.HistoriaId);
                return View(vm);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                AltaCapituloViewModel vm = CargarDatosFormulario(dto.HistoriaId);
                return View(vm);
            }






        }

        // Carga la historia y sus capitulos intermedios (los unicos que pueden ser antecedente).
        private AltaCapituloViewModel CargarDatosFormulario(int historiaId)
        {
            AltaCapituloViewModel vm = new AltaCapituloViewModel();

            Historia historia = _historias.FindById(historiaId);
            if (historia == null)
                throw new DominioException("Historia no encontrada.");


            vm.DtoHistoria = MapperHistoria.ToDtoHistoria(historia);
            vm.Dto = new DTOAltaCapitulo();
            
            vm.CapitulosIntermedios = MapperCapitulo.ToListDtoCapitulo(historia.Capitulos); 

            return vm;
        }
    }
}
