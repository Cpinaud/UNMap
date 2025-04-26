using MapaCampusMini.Models;
using MapaCampusMini.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MapaCampusMini.Controllers
{
    public class MapaNewController : Controller
    {

        private readonly MapaService _mapaService;
        public MapaNewController(MapaService mapaService)
        {
            _mapaService = mapaService;
        }

        public IActionResult Index()
        {
            var nodos = _mapaService.ObtenerTodosLosNodos();
            ViewBag.Nodos = nodos;
            return View();
        }

        [HttpPost]
        public IActionResult CalcularRuta(string origen, string destino)
        {
            var ruta = _mapaService.CalcularRuta(origen, destino);
            ViewBag.Nodos = _mapaService.ObtenerTodosLosNodos();
            ViewBag.Ruta = ruta;
            return View("Index");
        }
        [HttpPost]
        public JsonResult RecalcularRuta([FromBody] RequestRecRuta request)
        {
            var nuevaRuta = _mapaService.CalcularRuta(request.Actual, request.Destino);
            return Json(new { ruta = nuevaRuta });
        }


    }
}




