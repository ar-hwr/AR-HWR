/// <summary>
/// Class representing the object structure which comes from the server
/// </summary>
[System.Serializable]
public class Data
{
    public byte id;
    public string name;
    public string color;
    public byte match_id;
    public byte subway_tickets;
    public byte bus_tickets;
    public byte bike_tickets;
    public Connection[] connections;
    public byte position_node;
    public string position_name;
}
