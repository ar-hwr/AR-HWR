using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MatchData
{
    public byte id;
    public string name;
    public string state;
    public byte mr_x_id;
    public byte current_player_id;
    public byte rounds_left;
    public byte mr_x_last_node_id;
    //is last ticket string?
    public string mr_x_last_ticket;
    public List<PlayerInGame> players;
    public string mr_x_last_node_name;
    public string current_player_name;
}

[Serializable]
public class PlayerInGame
{
    public byte id;
    public string name;
    public string color;
    public byte subway_tickets;
    public byte bus_tickets;
    public byte bike_tickets;
    public byte positions_node;
    public string position_name;
}

public class PlayerPosition
{
    public string prefab;
    public string position;
}


public class RenderPlayers : MonoBehaviour
{
    //private List<PlayerPosition> playersToRender = new List<PlayerPosition>();
    //private List<PlayerPosition> playersToRenderInRoundBefore = new List<PlayerPosition>();

    private Dictionary<string, string> playersToRender = new Dictionary<string, string>();
    private Dictionary<string, string> playersToRenderInRoundBefore = new Dictionary<string, string>();
    GetRequestHandler getRequestHandler = new GetRequestHandler();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RefreshServerData", 20.0f, 20.0f);
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Mapping of colors and player prefabs
    /// </summary>
    private readonly Dictionary<string, string> playerColorPlayerName = new Dictionary<string, string>
    {
        { "black", "thief" },
        { "blue", "police blue" },
        { "green", "police green" },
        { "red", "police red" },
        { "yellow", "police yellow" }
    };


    void RefreshServerData()
    {
        StartCoroutine(getRequestHandler.FetchResponseFromWeb("http://finalnothing.net:9292/matches/ID?match_name=SamstagNachmittag2", result => RenderPlayer(result)));

    }

    void RenderPlayer(MatchData matchData)
    {
        playersToRender.Clear();

        foreach (var player in matchData.players)
        {
            if (player.position_name != String.Empty)
            {
                playersToRender.Add(playerColorPlayerName[player.color], player.position_name);
            }
        }

        foreach (var player in playersToRender)
        {
            //Hide players that moved
            string value;
            playersToRenderInRoundBefore.TryGetValue(player.Key, out value);
            if (value != null && value != player.Value)
            {
                GameObject.Find(value + "/" + player.Key).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
            }

            //render players that are still there
            GameObject.Find(player.Value + "/" + player.Key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        playersToRenderInRoundBefore = new Dictionary<string, string>(playersToRender);

    }
}
