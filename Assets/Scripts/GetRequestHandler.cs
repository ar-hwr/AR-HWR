using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Web;


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

[Serializable]
public class Test
{
    public int i1;
    public int i2;
    public string s;
    public bool b;
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
    private string serverURL2 = "http://finalnothing.net:9292/players/2";
    private string serverURL1 = "http://finalnothing.net:9292/players/1";

    public IEnumerator GetText2(Text debugText, Action<Data> result)
    {
        // user UnityWebRequest and not Core WebRequests when working with Unity
        WWW request = new WWW(serverURL2);
        yield return request;

            if (result != null)
            {
                debugText.text = "result is not null";
                Data data = JsonConvert.DeserializeObject<Data>(request.text);
                result(data);
            }
        
    }



    public IEnumerator FetchResponseFromWeb(Text debugText, Action<Data> callback)
    {
        debugText.text = "started Fetch Response";
        //WWWForm form = new WWWForm();

        WWW www = new WWW(serverURL2);//, form);

        // ‘done’ will be null until the information has been fetched from the web or there is an error
        yield return www;

        // Make sure all code paths set the callback
        if (www.error != null)
        {
            debugText.text = "error";
            callback(new Data());
        }
        else
        {
            debugText.text = "successfull";
            //callback(www.text);
            callback(JsonUtility.FromJson<Data>(www.text));
        }
    }

 


    public IEnumerator GetText1(Action<Data> result)
    {
        // user UnityWebRequest and not Core WebRequests when working with Unity
        UnityWebRequest request = UnityWebRequest.Get(serverURL1);
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