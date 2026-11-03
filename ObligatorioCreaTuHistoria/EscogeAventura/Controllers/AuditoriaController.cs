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

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
                return RedirectToAction("Login", "Auth");

            List<DTOAuditoria> auditorias = _cuListarAudi.Ejecutar();
            return View(auditorias);
        }
    }
}
