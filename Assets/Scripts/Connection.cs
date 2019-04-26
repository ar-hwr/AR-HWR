﻿/// <summary>
/// Class representing the object structure which comes from the server
/// </summary>
[System.Serializable]
public class Connection
{
    public byte id;
    public string type;
    public string destination_name;
    public byte destination_id;
}