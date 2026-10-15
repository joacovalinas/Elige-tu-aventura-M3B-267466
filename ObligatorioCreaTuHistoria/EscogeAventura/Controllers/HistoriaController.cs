using DTOs.DTO.DTOsHistoria;
using EscogeAventura.Controllers;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.CustomExceptions.Historia;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


public class HistoriaController : Controller
{
    private  ICUAltaHistoria _CuAltaHistoria;
    private ICUObtenerCategorias _cuObtenerCategorias;
    private  ICUListarHistoria _listarHistorias;
    private ICUEditarHistoria _cuEditarHistoria;
    private ICUObtenerHistoria _cuObtenerHistoria;
    private ICUPublicar _cuPublicar;

    public HistoriaController(ICUListarHistoria cUListarHistoria, ICUAltaHistoria cuAltaHistoria, 
        ICUObtenerCategorias cuObtenerCategorias, ICUEditarHistoria cuEditarHistoria, ICUObtenerHistoria cuObtenerHistoria,
        ICUPublicar cuPublicar)
    {
        _CuAltaHistoria = cuAltaHistoria;

        _cuObtenerCategorias = cuObtenerCategorias;
        _cuEditarHistoria = cuEditarHistoria;
        _cuObtenerHistoria = cuObtenerHistoria;
        _listarHistorias = cUListarHistoria;
        _cuPublicar = cuPublicar;
    }
    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
            return RedirectToAction("Login", "Auth");

        List<DTOHistoria> historias = _listarHistorias.Ejecutar();
        return View(historias);
    }

    [HttpGet]
    public IActionResult AltaHistoria()
    {
        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
            return RedirectToAction("Login", "Auth");

        AltaHistoriaViewModel vm = new AltaHistoriaViewModel();
        vm.Categorias = _cuObtenerCategorias.Ejecutar();
        

        return View(vm);
    }

    [HttpPost]
    public IActionResult AltaHistoria(AltaHistoriaViewModel vm)
    {
        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
            return RedirectToAction("Login", "Auth");
        try
        {
            
            _CuAltaHistoria.Ejecutar(vm.Dto);
            TempData["Exito"] = "Historia creada correctamente.";
            return RedirectToAction("Index"); // el Index del listado (RF03 consulta)
        }
        catch (DominioException e)
        {
            TempData["Error"] = e.Message;
            vm.Categorias = _cuObtenerCategorias.Ejecutar();
            return View(vm);
        }
        catch (DatoVacioException e)
        {

            TempData["Error"] = e.Message;
            vm.Categorias = _cuObtenerCategorias.Ejecutar();
            return View(vm);

        }
        catch (HistoriaYaExisteException e)
        {
            TempData["Error"] = e.Message;
            vm.Categorias = _cuObtenerCategorias.Ejecutar();
            return View(vm);
        }
        catch (Exception e)
        {

            TempData["Error"] = "Ocurrió un error inesperado.";
            vm.Categorias = _cuObtenerCategorias.Ejecutar();
            return View(vm);

        }

    }


    [HttpGet]
    public IActionResult EditarHistoria(int id)
    {
        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
            return RedirectToAction("Login", "Auth");



        List<string> categorias = _cuObtenerCategorias.Ejecutar().Select(c => c.Nombre).ToList();
        DTOHistoria dto = _cuObtenerHistoria.Ejecutar(id);
        dto.CatHistoria = categorias;

        if (dto == null)
        {
           
            TempData["Error"] = "No se encontró la historia.";
            return RedirectToAction("Index");
        }
        return View(dto);
    }


    [HttpPost]
    public IActionResult EditarHistoria(DTOHistoria dto)
    {
        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
            return RedirectToAction("Login", "Auth");

        try
        {
            _cuEditarHistoria.Ejecutar(dto);
            TempData["Exito"] = "Historia editada correctamente.";
            return RedirectToAction("Index");
        }
        catch (DbUpdateException e)
        {
            List<string> categorias = _cuObtenerCategorias.Ejecutar().Select(c => c.Nombre).ToList();
            dto.CatHistoria = categorias;
            TempData["Error"] = "El nombre de esta historia ya existe";
            return View(dto);
        }

        catch (Exception e)
        {
            List<string> categorias = _cuObtenerCategorias.Ejecutar().Select(c => c.Nombre).ToList();
            dto.CatHistoria = categorias;
            TempData["Error"] = e.Message;
            return View(dto);
        }
    }

    [HttpPost]
    public IActionResult Publicar(int id)
    {

        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
            return RedirectToAction("Login", "Auth");

        DTOHistoria dto = _cuObtenerHistoria.Ejecutar(id);

        try
        {
            _cuPublicar.Ejecutar(dto);
            TempData["Exito"] = "Historia publicada correctamente.";
            return RedirectToAction("Index");
        }
        catch (DbUpdateException e)
        {
            
            TempData["Error"] = "El nombre de esta historia ya existe";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
           
            TempData["Error"] = e.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult AgregarCapitulo(int id)
    {
        if (HttpContext.Session.GetString("LogueadoRol") != "Admin")
           return RedirectToAction("Login", "Auth");

        return RedirectToAction("AltaCapitulo", "Capitulo", new { historiaId  = id});
    }


}