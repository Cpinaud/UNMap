using MapaCampusMini.Models;

namespace MapaCampusMini.Services.Interfaces
{
    public interface ILecturaJsonService
    {
        Dictionary<string, Nodo> CargarNodosDesdeJson();
    }
}
