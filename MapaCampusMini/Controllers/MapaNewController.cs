using MapaCampusMini.Models;
using MapaCampusMini.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MapaCampusMini.Controllers
{
    [ApiController]
    [Route("MapaNew")]
    public class MapaNewController : ControllerBase
    {

        private readonly IMapaService _mapaService;
        public MapaNewController(IMapaService mapaService)
        {
            _mapaService = mapaService;
        }

       
        [HttpGet("ObtenerNodos")]
        public IActionResult ObtenerNodos()
        {
            var nodos = _mapaService.ObtenerTodosLosNodos();
            
            return new JsonResult(nodos);
        }

        [HttpPost("CalcularNodo")]
        public IActionResult CalcularRuta(string origen, string destino)
        {
            var ruta = _mapaService.CalcularRuta(origen, destino);
            var nodos = _mapaService.ObtenerTodosLosNodos();
            var response = new
            {
                Nodos = nodos,
                Ruta = ruta,
                OrigenSeleccionado = origen,
                DestinoSeleccionado = destino
            };
            return new JsonResult(response);
        }
        [HttpPost("RecalcularRuta")]
        public JsonResult RecalcularRuta([FromBody] RequestRecRuta request)
        {
            var nuevaRuta = _mapaService.CalcularRuta(request.Actual, request.Destino);
            return new JsonResult(new { ruta = nuevaRuta });
        }


    }
}




