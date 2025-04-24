using MapaCampusMini.Models;

namespace MapaCampusMini.Services;

public class MapaService
{
    private readonly Dictionary<string, Nodo> _nodos;

    public MapaService()
    {
        _nodos = new()
        {
            ["A"] = new Nodo { Id = "A", ConectadoCon = new() { ["B"] = 5, ["C"] = 10 } },
            ["B"] = new Nodo { Id = "B", ConectadoCon = new() { ["A"] = 5, ["D"] = 3 } },
            ["C"] = new Nodo { Id = "C", ConectadoCon = new() { ["A"] = 10, ["D"] = 4 } },
            ["D"] = new Nodo { Id = "D", ConectadoCon = new() { ["B"] = 3, ["C"] = 4, ["E"] = 2 } },
            ["E"] = new Nodo { Id = "E", ConectadoCon = new() { ["D"] = 2 } }
        };
    }

    public List<Nodo> ObtenerTodosLosNodos()
    {
        return _nodos.Values.ToList();
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

        // Inicializar distancias a infinito, excepto el nodo inicial
        foreach (var nodo in _nodos)
        {
            distancias[nodo.Key] = int.MaxValue;
        }
        distancias[origen] = 0;

        while (nodosNoVisitados.Count > 0)
        {
            // Obtener el nodo no visitado con la menor distancia conocida
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
