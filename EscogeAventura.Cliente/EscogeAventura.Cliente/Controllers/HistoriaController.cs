using EscogeAventura.Cliente.Models;
using EscogeAventura.Cliente.Models.DTOs.DTOsCategoria;
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
        public async Task<IActionResult> Index(int? categoriaId)
        {
            string token = HttpContext.Session.GetString("Token");
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Solo manda el parámetro si hay una categoría válida
            var url = categoriaId is > 0
                ? $"Historia/Listar?categoriaId={categoriaId}"
                : "Historia/Listar";

            var response = await _cliente.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Error al obtener las historias. Por favor, inténtelo de nuevo";
                return View(new DTOIndexHistoria());
            }

            var json = await response.Content.ReadAsStringAsync();
            var modelo = JsonSerializer.Deserialize<DTOIndexHistoria>(json, options) ?? new DTOIndexHistoria();

            // Categorías para el desplegable
            var respCat = await _cliente.GetAsync("Categoria/Listar");
            if (respCat.IsSuccessStatusCode)
            {
                var jsonCat = await respCat.Content.ReadAsStringAsync();
                modelo.Categorias = JsonSerializer.Deserialize<List<DTOCategoria>>(jsonCat, options) ?? new();
            }

            modelo.CategoriaId = categoriaId;
            return View(modelo);


        }


      






    }
}
