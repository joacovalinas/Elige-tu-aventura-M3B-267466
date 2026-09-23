using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.WebApp.Controllers
{
    public class AuthController : Controller
    {

        private ICUVerifExistencia _CuVerifExistencia;

        public AuthController(ICUVerifExistencia CuVerificarExistencia)
        {
            _CuVerifExistencia = CuVerificarExistencia;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {

            DTOUsuario u = _CuVerifExistencia.Ejecutar(vm.Email, vm.Password);

            if (u != null)
            {
                HttpContext.Session.SetInt32("LogueadoId", u.Id);
                HttpContext.Session.SetString("LogueadoEmail", u.Email);
                HttpContext.Session.SetString("LogueadoRol", u.Rol.Nombre);
                HttpContext.Session.SetString("LogueadoNombre", u.Nombre);
                return RedirectToAction("Index", "Home");
            }


            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}
