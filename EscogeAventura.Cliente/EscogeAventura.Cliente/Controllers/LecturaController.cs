using EscogeAventura.Cliente.Models.DTOs.DTOsHistoria;
using EscogeAventura.Cliente.Models.DTOs.DTOsLectura;
using EscogeAventura.Cliente.Models.DTOsLectura;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<IActionResult> AltaLectura(int histId, string Nombrelectura)
        {

            DTOAltalectura dtoAltalectura = new DTOAltalectura
            {
                Nombrelectura = Nombrelectura,
                UsuarioId = (int)HttpContext.Session.GetInt32("LogueadoId"),
                HistoriaId = histId,
                FechaInicio = DateTime.Now
            };

            var json = JsonSerializer.Serialize(dtoAltalectura);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string token = HttpContext.Session.GetString("Token");

            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _cliente.PostAsync("Lectura/AltaLectura", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Leer", new { nombreLectura = Nombrelectura });

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");

            TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Historia"); // o donde corresponda
            
        }


          public async Task<IActionResult> Leer(int nombreLectura, int IdLectura)
          {
              string token = HttpContext.Session.GetString("Token");
              _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
           
            var response = await _cliente.GetAsync($"Lectura/{nombreLectura}?idUsuario={IdLectura}");

           if (response.IsSuccessStatusCode)
              {
                  var json = await response.Content.ReadAsStringAsync();

                  var options = new JsonSerializerOptions
                  {
                      PropertyNameCaseInsensitive = true
                  
                  };

                DTOLeer lectura = new DTOLeer();
                lectura = JsonSerializer.Deserialize<DTOLeer>(json, options);

                  return View(lectura);
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

        public async Task<IActionResult> Avanzar(DTOLeer dto)
        {

            var json = JsonSerializer.Serialize(dto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string token = HttpContext.Session.GetString("Token");

            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _cliente.PostAsync("Lectura/Avanzar/" + dto.nombreLectura, content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Leer", new { nombreLectura = dto.nombreLectura });

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");

            TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
            return View("Leer",dto); // o donde corresponda


        }
    }
}
