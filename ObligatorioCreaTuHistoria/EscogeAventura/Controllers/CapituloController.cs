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


            if (_historias.FindById(historiaId) == null)
               return NotFound();

            AltaCapituloViewModel vm = CargarDatosFormulario(historiaId);

            return View(vm);
        }

        [HttpPost]
        
        public IActionResult AltaCapitulo(DTOAltaCapitulo dto)
        {
            try
            {
                //int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _CuAltaCapitulo.Ejecutar(dto);
                TempData["Mensaje"] = "Capítulo agregado.";
                AltaCapituloViewModel vm = CargarDatosFormulario(dto.HistoriaId);

                return View(vm);
            }
            catch (DominioException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
