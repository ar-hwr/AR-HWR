using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// Class representing the object structure which comes from the server
/// TODO move this to another .cs file
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

[Serializable]
public class Connection
{
    public byte id;
    public string type;
    public string node_name;
}

public class GetRequestHandler : MonoBehaviour
{
    /// <summary>
    /// Basic method for get requests to server
    /// </summary>
    /// <param name="result">contains the server's response</param>
    /// <returns>the server's response or an error code as soon as the server answers (yield)</returns>
    /// 
    // set server url of custom API here
    private string serverURL = "http://finalnothing.net:9292/players/2";

    public IEnumerator GetText(Action<Data> result)
    {
        // user UnityWebRequest and not Core WebRequests when working with Unity
        UnityWebRequest request = UnityWebRequest.Get(serverURL);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            if (result != null)
            {
                Data data = JsonConvert.DeserializeObject<Data>(request.downloadHandler.text);
                result(data);
            }
        }
    }
}