using EscogeAventura.Cliente.Models.DTOs.DTOsHistoria;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EscogeAventura.Cliente.Controllers
{
    public class LecturaController : Controller
    {
        private HttpClient _cliente;
        public LecturaController(HttpClient cliente)
        {
            _cliente = cliente;
            _cliente.BaseAddress = new Uri("http://localhost:5172/api/");
        }

        [HttpPost]
        public async Task<IActionResult> AltaLectura(DTOHistoria dto)
        {
            var json = JsonSerializer.Serialize(dto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string token = HttpContext.Session.GetString("Token");
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _cliente.PostAsync("Lectura/AltaLectura", content);

            return View("Leer",content);
        }
    }
}
