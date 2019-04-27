using System;
using System.Collections;
using UnityEngine;

public class GetRequestHandler : MonoBehaviour
{
    // set server url of custom API here
    private string serverURL2 = "http://finalnothing.net:9292/players/2";
    private string serverURL1 = "http://finalnothing.net:9292/players/1";

    /// <summary>
    /// Simple get request to server
    /// </summary>
    /// <param name="callback">What to do with the requests answer</param>
    /// <returns></returns>
    public IEnumerator FetchResponseFromWeb(string url, Action<MatchData> callback)
    {
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            callback(new MatchData());
        }
        else
        {
            callback(JsonUtility.FromJson<MatchData>(www.text));
        }
    }
}