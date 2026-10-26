using EscogeAventura.Cliente.Models;
using EscogeAventura.Cliente.Models.DTOs.DTOsHistoria;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EscogeAventura.Cliente.Controllers
{
    public class HistoriaController : Controller
    {

        private HttpClient _cliente;
        public HistoriaController(HttpClient cliente)
        {
            _cliente = cliente;
            _cliente.BaseAddress = new Uri("http://localhost:5172/api/");
        }



        [HttpGet]
        public async Task<IActionResult> Index(DTOIndexHistoria dto)
        {

            
               

                string token = HttpContext.Session.GetString("Token");
                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


     
                var response = await _cliente.GetAsync("Historia/Listar");




                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    DTOIndexHistoria historias = JsonSerializer.Deserialize<DTOIndexHistoria>(json, options);

                    return View(historias);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Auth");
                }
            else
            {
                TempData["ErrorMessage"] = "Error al obtener las historias. Por favor, inténtelo de nuevo";
            }
            return View();
            
           


          
        }
    }
}
