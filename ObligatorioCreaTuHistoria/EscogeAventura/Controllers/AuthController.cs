using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.WebApp.Controllers
{
    public class AuthController : Controller
    {

        private ICUVerifExistenciaAdmin _CuVerifExistenciaAdmin;

        public AuthController(ICUVerifExistenciaAdmin CuVerificarExistenciaAdmin)
        {
            _CuVerifExistenciaAdmin = CuVerificarExistenciaAdmin;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            try
            {
                DTOUsuario u = _CuVerifExistenciaAdmin.Ejecutar(vm.Email, vm.Password);

                if (u != null)
                {
                    HttpContext.Session.SetInt32("LogueadoId", u.Id);
                    HttpContext.Session.SetString("LogueadoEmail", u.Email);
                    HttpContext.Session.SetString("LogueadoRol", u.Rol.Nombre);
                    HttpContext.Session.SetString("LogueadoNombre", u.Nombre);
                    return RedirectToAction("Index", "Home");
                }
            }catch (DatoVacioException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (DominioException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception)
            {

                return View("Error");
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
