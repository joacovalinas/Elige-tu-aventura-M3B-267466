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
            try
            {
                //Comienzo a configurar la request
                //1 - Paso el dto a JSON
                var json = JsonSerializer.Serialize(dto);

                //2 - Genero el contenido para enviar
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //3 - Hago la request
                var response = await _cliente.PostAsync("auth/login", content);



                /* ESTO ES LO MISMO DE ARRIBA PERO EN UNA SOLA LÍNEA
                   var response = await _client.PostAsync("auth/login", 
                    new StringContent(JsonSerializer.Serialize(dto), 
                    Encoding.UTF8, "application/json"));
                 */


                if (response.IsSuccessStatusCode)
                {

                    string result = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    LoginResponse lr = JsonSerializer.Deserialize<LoginResponse>(result, options);

                    HttpContext.Session.SetInt32("LogueadoId", lr.IdUsuario);
                    HttpContext.Session.SetString("LogueadoRol", lr.Rol);
                    HttpContext.Session.SetString("LogueadoNombre", lr.Nombre);
                    HttpContext.Session.SetString("Token", lr.token);

                    string token = HttpContext.Session.GetString("Token");

                    return RedirectToAction("Index", "Home");

                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.msg = "Login Incorrecto";
                    return View();
                }
                else
                {
                    ViewBag.msg = "Error inesperado";
                    return View();
                }

            }
            catch (Exception e)
            {

                ViewBag.msg = "Error inesperado";
                return View();
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");


        }
    }
}
