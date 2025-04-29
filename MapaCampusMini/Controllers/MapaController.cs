using MapaCampusMini.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapaCampusMini.Controllers;

[ApiController]
[Route("Mapa")]
public class MapaController : ControllerBase
{
    private readonly IMapaService _mapaService;

    public MapaController(MapaService mapaService)
    {
        _mapaService = mapaService;
    }

    [HttpGet("ruta")]
    public ActionResult<List<string>> ObtenerRuta([FromQuery] string origen, [FromQuery] string destino)
    {
        var ruta = _mapaService.CalcularRuta(origen, destino);
        if (ruta == null || ruta.Count == 0)
            return NotFound("No se encontró una ruta.");

        return Ok(ruta);
    }
}
