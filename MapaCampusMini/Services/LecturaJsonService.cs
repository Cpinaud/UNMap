using MapaCampusMini.Models;
using MapaCampusMini.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace MapaCampusMini.Services

{
    public class LecturaJsonService : ILecturaJsonService
    {
        public Dictionary<string, Nodo> CargarNodosDesdeJson()
        {
            //Inicializo el Dictionary de nodos
            var nodos = new Dictionary<string, Nodo>();
            //creo un nodo vacío para poder usarlo y cargar el Dictionary
          //  var nodoObj = new Nodo();

            //Leo el json de nodos, lo parseo y guardo en nodosList la lista de nodos (prop features del json)
            var nodosJson = File.ReadAllText("Data/Nodos.geojson");
            var geoJsonNodos = JObject.Parse(nodosJson);
            var nodosList = geoJsonNodos["features"];
            
            //Leo el json de lineas, lo parseo y guardo en lineasList la lista de lineas (prop features del json)
            var lineasJson = File.ReadAllText("Data/Lineas.geojson");
            var geoJsonLineas = JObject.Parse(lineasJson);
            var lineasList = geoJsonLineas["features"];

            //Recorro la lista de nodos y por cada nodo guardo sus propiedades
            foreach (var nodo in nodosList)
            {
                var id = nodo["properties"]["ID"].ToString();
                var Nombre = nodo["properties"]["Nombre"].ToString();
                var Tipo = nodo["properties"]["Tipo"].ToString();
                var Piso = Convert.ToInt32(nodo["properties"]["Piso"]);
                var coordenadas = nodo["geometry"]["coordinates"].ToObject<List<double>>();
                var x = (int)coordenadas[0];
                var y = (int)coordenadas[1];
                var conexiones = nodo["properties"]["Conexion"].ToObject<List<string>>();
                //creo el Dictionary para el campo ConectadoCon
                var ConectadoCon = new Dictionary<string, int>();
                //por cada nodo con el que conecta el nodo iterado, busco la linea en el json de lineas y guardo la distancia
                foreach (var nodoConect in conexiones)
                {
                    foreach (var linea in lineasList)
                    {
                        //guardo los dos nodos que conectan la linea iterada
                        var nodosConect = linea["properties"]["Nodos"].ToObject<List<string>>();
                        //si la linea iterada es la que conecta el nodo inicial con el nodoConect iterado, guardo su longitud
                        if (nodosConect.Contains(nodoConect) && nodosConect.Contains(id))
                        {
                            var distancia = Convert.ToInt32(linea["properties"]["Longitud"]);
                            //cuando la encuentro, la elimino de la lista de lineas ya que no la necesito mas. Asi optimizo la búsqueda
                            //lineasList.Remove(linea); //esto da error porque no se puede modificar la colección mientras se recorre con foreach
                            //TO DO: VER CÓMO HACERLO
                            ConectadoCon.Add(nodoConect, distancia); 
                            break;
                        }
                    }
                    
                }

                //cargo el objeto Nodo y lo guardo en el diccionario
                var nodoObj = new Nodo
                {
                    Id = id,
                    Nombre = Nombre,
                    Tipo = Tipo,
                    Piso = Piso,
                    X = x,
                    Y = y,
                    ConectadoCon = ConectadoCon
                };

                nodos[id] = nodoObj;


            }
            
            return nodos;
        }
    }
}
