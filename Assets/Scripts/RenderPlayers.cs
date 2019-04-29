using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPlayers : MonoBehaviour
{
    private Dictionary<string, string> playersToRender = new Dictionary<string, string>();
    private Dictionary<string, string> playersToRenderInRoundBefore = new Dictionary<string, string>();

    GetRequestHandler getRequestHandler = new GetRequestHandler();

    public static string gameName = String.Empty;

    void Start()
    {
        InvokeRepeating("RefreshServerData", 2.0f, 1.0f);
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


    public void RefreshServerData()
    {
        StartCoroutine(getRequestHandler.FetchResponseFromWeb(gameName, result => RenderPlayer(result)));
    }

    void RenderPlayer(MatchData matchData)
    {
        if (matchData.state == "running")
        {
            if (playersToRender != playersToRenderInRoundBefore)
            {
                playersToRender.Clear();

                foreach (var player in matchData.players)
                {
                    if (player.position_name == String.Empty)
                    {
                        //it is a thief
                        if (matchData.mr_x_last_node_name != String.Empty)
                        {
                            playersToRender.Add(playerColorPlayerName["black"], matchData.mr_x_last_node_name);
                        }
                    }
                    else
                    {
                        //it is a police
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

                    //rendering a thief with unknown position
                    //a thief for that the position is not actualized is displayed with a darker skin
                    //this way, users see, that the thief's shown position is the place where he was discovered for the last time    
                    if (player.Key == "thief")
                    {
                        string posThiefRoundBefore;
                        playersToRenderInRoundBefore.TryGetValue("thief", out posThiefRoundBefore);
                        if (posThiefRoundBefore != null)
                        {
                            if (posThiefRoundBefore == player.Value)
                            {
                                GameObject.Find(player.Value + "/" + "thief dark").GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            }
                            else
                            {
                                GameObject.Find(player.Value + "/" + player.Key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            }
                        }
                    }
                    else
                    {
                        //rendering other players
                        GameObject.Find(player.Value + "/" + player.Key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }

                playersToRenderInRoundBefore = new Dictionary<string, string>(playersToRender);
            }
        }
        else
        {
            if (matchData.state == "police_won")
            {
                StartViewing startViewing = new StartViewing();
                startViewing.OnPoliceWon();
            }

            if (matchData.state == "mr_x_won")
            {
                StartViewing startViewing = new StartViewing();
                startViewing.OnThiefWon();
            }
        }
    }
}
