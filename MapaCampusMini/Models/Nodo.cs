namespace MapaCampusMini.Models;

public class Nodo
{
    public string Id { get; set; }
    
    public Dictionary<string, int> ConectadoCon { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}