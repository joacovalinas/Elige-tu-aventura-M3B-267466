using DTOs.DTO.DTOsHistoria;
using EscogeAventura.Controllers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//[Authorize(Roles = "Admin")]
public class HistoriaController : Controller
{
    private  ICUAltaHistoria _CuAltaHistoria;
    private  IRepositorioCategoria _RepoCategorias;
    private ICUObtenerCategorias _cuObtenerCategorias;
    private  ICUListarHistoria _listarHistorias;

    public HistoriaController(ICUListarHistoria cUListarHistoria, ICUAltaHistoria cuAltaHistoria, IRepositorioCategoria categorias, ICUObtenerCategorias cuObtenerCategorias)
    {
        _CuAltaHistoria = cuAltaHistoria;
        _RepoCategorias = categorias;
        _cuObtenerCategorias = cuObtenerCategorias;
        _listarHistorias = cUListarHistoria;
    }

    [HttpGet]
    public IActionResult AltaHistoria()
    {
        AltaHistoriaViewModel vm = new AltaHistoriaViewModel();
        vm.Categorias = _cuObtenerCategorias.Ejecutar();
        

        return View(vm);
    }

    [HttpPost]
    
    public IActionResult AltaHistoria(AltaHistoriaViewModel vm)
    {
        try
        {
            
            _CuAltaHistoria.Ejecutar(vm.Dto);

            return RedirectToAction("Index"); // el Index del listado (RF03 consulta)
        }
        catch (DominioException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            vm.Categorias = _cuObtenerCategorias.Ejecutar();
            return View(vm);
        }
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<DTOHistoria> historias = _listarHistorias.Ejecutar();
        return View(historias);
    }


    [HttpPost]
   
    public IActionResult AgregarCapitulo(int id)
    {
       
        return RedirectToAction("AltaCapitulo", "Capitulo", new { historiaId  = id});
    }


}