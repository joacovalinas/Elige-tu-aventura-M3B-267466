using DTOs.DTO.DTOsAuditoria;
using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using Microsoft.AspNetCore.Mvc;

namespace EscogeAventura.WebApp.Controllers
{
    public class AuditoriaController : Controller
    {
        private ICUListarAuditoria _cuListarAudi;

        public AuditoriaController(ICUListarAuditoria cuListarAudi)
        {
            _cuListarAudi = cuListarAudi;
        }

        public IActionResult Index(string? usuario, string? historia)
        {
            if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
                return RedirectToAction("Login", "Auth");

            List<DTOAuditoria> auditorias = _cuListarAudi.Ejecutar();

            // Solo se aplica un filtro a la vez (si llegaran los dos, manda usuario)
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                string u = usuario.Trim();
                auditorias = auditorias
                    .Where(a => a.Usuario != null &&
                                a.Usuario.Contains(u, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else if (!string.IsNullOrWhiteSpace(historia))
            {
                string h = historia.Trim();
                auditorias = auditorias
                    .Where(a => a.HistoriaNombre != null &&
                                a.HistoriaNombre.Contains(h, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(auditorias);
        }
    }
}
