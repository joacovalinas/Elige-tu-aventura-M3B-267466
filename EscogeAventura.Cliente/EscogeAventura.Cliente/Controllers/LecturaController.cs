using EscogeAventura.Cliente.Models.DTOs.DTOsHistoria;
using EscogeAventura.Cliente.Models.DTOs.DTOsLectura;
using EscogeAventura.Cliente.Models.DTOsLectura;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
            {
                var body = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<DTORespuestaAltaLectura>(body, opciones);

                return RedirectToAction("Leer", new { idLectura = resultado.IdLectura });
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");

            TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Historia"); // o donde corresponda

        }


        public async Task<IActionResult> Leer(int nombreLectura, int idLectura)
        {
            string token = HttpContext.Session.GetString("Token");
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.GetAsync($"Lectura/{idLectura}");

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

        [HttpPost]
        public async Task<IActionResult> Avanzar(DTOLeer dto)
        {

            var json = JsonSerializer.Serialize(dto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string token = HttpContext.Session.GetString("Token");

            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _cliente.PostAsync("Lectura/Avanzar", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Leer", new { idLectura = dto.Id });

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");

            TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
            return View("Leer", dto); // o donde corresponda


        }



        [HttpGet]
        public async Task<IActionResult> Historial(string? mes)
        {
            int? usuarioId = HttpContext.Session.GetInt32("LogueadoId");
            string token = HttpContext.Session.GetString("Token");

            if (usuarioId == null || string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            // El <input type="month"> manda "yyyy-MM"
            DateTime? fecha = null;
            if (!string.IsNullOrWhiteSpace(mes))
            {
                if (DateTime.TryParseExact(mes, "yyyy-MM", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime parsed))
                {
                    fecha = parsed;
                }
                else
                {
                    ViewBag.Error = "El mes ingresado no es válido";
                }
            }

            ViewBag.Mes = mes;

            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string url = $"Lectura/historial/{usuarioId}";
            if (fecha.HasValue)
                url += $"?fecha={fecha.Value:yyyy-MM-dd}";

            var response = await _cliente.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                List<DTOLecturaHistorial> lecturas =
                    JsonSerializer.Deserialize<List<DTOLecturaHistorial>>(json, options)
                    ?? new List<DTOLecturaHistorial>();

                return View(lecturas);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");

            ViewBag.Error = "Error al obtener el historial. Por favor, inténtelo de nuevo";
            return View(new List<DTOLecturaHistorial>());
        }



        [HttpGet]
        public async Task<IActionResult> RankingHistorias()
        {
            string token = HttpContext.Session.GetString("Token");
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _cliente.GetAsync($"Lectura/Ranking");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var lista = JsonSerializer.Deserialize<List<DTORankingHistorias>>(json,
                    new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true,
                        IncludeFields = true
                    });

                return View(lista);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");
            ViewBag.Error = "Error al obtener el detalle de la lectura. Por favor, inténtelo de nuevo";
            return View(new List<DTORankingHistorias>());
        }




        private async Task<List<DTOHistoria>> ObtenerHistorias()
        {
            HttpResponseMessage respuesta = await _cliente.GetAsync("Historia/listar"); // poné la ruta real de tu API
            if (!respuesta.IsSuccessStatusCode)
                return null;

            string json = await respuesta.Content.ReadAsStringAsync();
            DTOIndexHistoria dto = JsonSerializer.Deserialize<DTOIndexHistoria>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (dto == null || dto.Historias == null)
                return new List<DTOHistoria>();

            return dto.Historias;
        }

        [HttpGet]
        public async Task<IActionResult> SeleccionarHistoria()
        {
            string token = HttpContext.Session.GetString("Token");
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            List<DTOHistoria> historias = await ObtenerHistorias();
            if (historias == null)
            {
                ViewBag.Error = "Error al obtener las historias. Por favor, inténtelo de nuevo";
                return View(new List<DTOHistoria>());
            }

            return View(historias);
        }

        [HttpGet]
        public async Task<IActionResult> FinalesAlcanzados(int historiaId)
        {
            string token = HttpContext.Session.GetString("Token");
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await _cliente.GetAsync($"Lectura/finales-alcanzados/{historiaId}");
            if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Auth");

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error al obtener los finales de la historia. Por favor, inténtelo de nuevo";
                return View(new List<DTOFinalAlcanzado>());
            }

            string json = await respuesta.Content.ReadAsStringAsync();
            List<DTOFinalAlcanzado> finales = JsonSerializer.Deserialize<List<DTOFinalAlcanzado>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            List<DTOHistoria> historias = await ObtenerHistorias();
            if (historias != null)
            {
                DTOHistoria elegida = historias.FirstOrDefault(h => h.Id == historiaId);
                if (elegida != null)
                    ViewBag.TituloHistoria = elegida.Titulo;
            }

            return View(finales);


        }

        }
}
