using System;
using System.Collections;
using UnityEngine;

public class GetRequestHandler : MonoBehaviour
{
    // set server url of custom API here
    private string urlPartOne = "http://finalnothing.net:9292/matches/ID?match_name=";

    /// <summary>
    /// Simple get request to server
    /// </summary>
    /// <param name="callback">What to do with the requests answer</param>
    /// <returns></returns>
    public IEnumerator FetchResponseFromWeb(string url, Action<MatchData> callback)
    {
        WWW www = new WWW(urlPartOne + url);
        yield return www;

        if (www.error != null)
        {
            MatchData matchData = new MatchData();
            matchData.state = "404";
            callback(matchData);
        }
        else
        {
            callback(JsonUtility.FromJson<MatchData>(www.text));
        }
    }
}