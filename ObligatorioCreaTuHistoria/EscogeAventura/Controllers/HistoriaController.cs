using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//[Authorize(Roles = "Admin")]
public class HistoriaController : Controller
{
    private readonly ICUAltaHistoria _CuAltaHistoria;
    private readonly IRepositorioCategoria _RepoCategorias;

    public HistoriaController(ICUAltaHistoria cuAltaHistoria, IRepositorioCategoria categorias)
    {
        _CuAltaHistoria = cuAltaHistoria;
        _RepoCategorias = categorias;
    }

    [HttpGet]
    public IActionResult AltaHistoria()
    {
        CargarCategorias();
        return View(new DTOAltaHistoria());
    }

    [HttpPost]
    
    public IActionResult AltaHistoria(DTOAltaHistoria dto)
    {
        try
        {
            
            _CuAltaHistoria.Ejecutar(dto);
            TempData["Mensaje"] = "Historia creada en estado BORRADOR.";
            return RedirectToAction("Index"); // el Index del listado (RF03 consulta)
        }
        catch (DominioException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            CargarCategorias();
            return View(dto);
        }
    }

    private void CargarCategorias()
    {
        ViewBag.Categorias = _RepoCategorias.FindAll();
    }
}