using EscogeAventura.Cliente.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EscogeAventura.Cliente.Controllers
{
    public class AuthController : Controller
    {
        private HttpClient _cliente;
        public AuthController(HttpClient cliente)
        {
            _cliente = cliente;
            _cliente.BaseAddress = new Uri("http://localhost:5172/api/");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(DTOLogin dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                ViewBag.msg = "Debe ingresar el email y la contraseña";
                return View(dto);
            }
            try
            {
            
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("auth/login", content);

                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    LoginResponse lr = JsonSerializer.Deserialize<LoginResponse>(result, options);

                    HttpContext.Session.SetInt32("LogueadoId", lr.IdUsuario);
                    HttpContext.Session.SetString("LogueadoRol", lr.Rol);
                    HttpContext.Session.SetString("LogueadoNombre", lr.Nombre);
                    HttpContext.Session.SetString("Token", lr.token);

                    return RedirectToAction("Index", "Home");
                }


                ViewBag.msg = response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => string.IsNullOrWhiteSpace(result) ? "Email o contraseña incorrectos" : result,
                    HttpStatusCode.BadRequest => string.IsNullOrWhiteSpace(result) ? "Los datos ingresados no son válidos" : result,
                    HttpStatusCode.InternalServerError => "Error en el servidor, intente más tarde",
                    _ => $"Error inesperado ({(int)response.StatusCode})"
                };

                dto.Contrasena = null; // no devolver la contraseña a la vista
                return View(dto);
            }
            catch (HttpRequestException)
            {
                ViewBag.msg = "No se pudo conectar con el servidor";
                return View(dto);
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View(dto);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");


        }
    }
}
