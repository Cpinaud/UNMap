using MapaCampusMini.Models;
using MapaCampusMini.Services.Interfaces;

namespace MapaCampusMini.Services;

public class MapaService
{
    private readonly Dictionary<string, Nodo> _nodos;

    public MapaService(ILecturaJsonService LecturaJsonService)
    {
        _nodos = LecturaJsonService.CargarNodosDesdeJson(); 
    }

    public List<Nodo> ObtenerTodosLosNodos()
    {
        return _nodos
            .Select(kv => {
                var nodo = kv.Value;
                nodo.Id = kv.Key;     
                return nodo;
            })
            .ToList();
    }

    public Nodo? ObtenerNodoPorId(string id)
    {
        return _nodos.ContainsKey(id) ? _nodos[id] : null;
    }

    public List<string> CalcularRuta(string origen, string destino)
    {
        var distancias = new Dictionary<string, int>();
        var anteriores = new Dictionary<string, string>();
        var nodosNoVisitados = new HashSet<string>(_nodos.Keys);

       
        foreach (var nodo in _nodos)
        {
            distancias[nodo.Key] = int.MaxValue;
        }
        distancias[origen] = 0;

        while (nodosNoVisitados.Count > 0)
        {
           
            var nodoActual = nodosNoVisitados.OrderBy(n => distancias[n]).First();

            // Si llegamos al destino, terminamos
            if (nodoActual == destino)
                break;

            nodosNoVisitados.Remove(nodoActual);

            var nodo = _nodos[nodoActual];

            // Recorrer vecinos y actualizar distancias
            foreach (var vecino in nodo.ConectadoCon)
            {
                if (!nodosNoVisitados.Contains(vecino.Key))
                    continue;

                int nuevaDistancia = distancias[nodoActual] + vecino.Value;

                if (nuevaDistancia < distancias[vecino.Key])
                {
                    distancias[vecino.Key] = nuevaDistancia;
                    anteriores[vecino.Key] = nodoActual;
                }
            }
        }

        // Reconstruir el camino desde destino a origen
        var ruta = new List<string>();
        string nodoRuta = destino;

        while (anteriores.ContainsKey(nodoRuta))
        {
            ruta.Insert(0, nodoRuta);
            nodoRuta = anteriores[nodoRuta];
        }

        if (nodoRuta == origen)
        {
            ruta.Insert(0, origen);
        }
        else
        {
            // No hay ruta
            return new List<string>();
        }

        return ruta;
    }
}
