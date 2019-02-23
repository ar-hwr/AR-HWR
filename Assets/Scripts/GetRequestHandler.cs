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
public class Test
{
    public string userId;
    public byte id;
    public string title;
    public bool completed;
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
    private string serverURL = "https://jsonplaceholder.typicode.com/todos/1";

    public IEnumerator GetText(Action<Test> result)
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
                Test test = JsonConvert.DeserializeObject<Test>(request.downloadHandler.text);
                result(test);
            }
        }
    }
}