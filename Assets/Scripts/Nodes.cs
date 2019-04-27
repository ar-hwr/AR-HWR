using System.Collections.Generic;

/// <summary>
/// Class representing the object structure which comes from the server
/// </summary>
[System.Serializable]
public class Nodes
{
    public string name;
    public List<Connection> connections;

}
