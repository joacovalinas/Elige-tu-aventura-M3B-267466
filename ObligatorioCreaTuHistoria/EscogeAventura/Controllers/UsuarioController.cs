using DTOs.DTO.DTOsRol;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICURol;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
using EscogeAventura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.WebApp.Controllers
{
    public class UsuarioController : Controller
    {

        private ICUObtenerRol _CUObtenerRol;
        private ICUAltaUsuario _CuAltaUsuario;

        public UsuarioController(ICUObtenerRol CUObtenerRol, ICUAltaUsuario CuAltaUsuario)
        {
            _CUObtenerRol = CUObtenerRol;
            _CuAltaUsuario = CuAltaUsuario;
        }


        public IActionResult AltaUsuario()
        {
            List<DTORol> roles = _CUObtenerRol.Ejecutar();

            AltaUsuarioViewModel vm = new AltaUsuarioViewModel();
            vm.TodosLosRoles = roles;

            return View(vm);

        }

        [HttpPost]
        public IActionResult AltaUsuario(AltaUsuarioViewModel vm)
        {
            try
            {
                _CuAltaUsuario.Ejecutar(vm.Dto);
                ViewBag.msg = "Alta correcta";

            }

            catch (DatoVacioException e)
            {

                ViewBag.msg = e.Message;

            }
            catch (UsuarioYaExisteException e)
            {
                ViewBag.msg = e.Message;

            }
            catch (Exception e)
            {

                ViewBag.msg = "Error";

            }
            
            vm.TodosLosRoles = _CUObtenerRol.Ejecutar();

            return View(vm);
        }



    }
}
