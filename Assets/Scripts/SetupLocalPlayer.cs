using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    [SyncVar]
    public string PlayerName;

    [HideInInspector]
    public static Dictionary<string, string> PlayerNamePlayerPosition = new Dictionary<string, string>();

    [HideInInspector]
    public Color PlayerColor;

    [HideInInspector]
    public string PlayerPrefab;

    //[SyncVar(hook = "OnChangePlayerList")]
    //[SyncVar]
    [HideInInspector]
    public static string Players;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerName);

        foreach (var player in PlayerNamePlayerPosition)
        {
            RenderPlayer(player);
            Debug.Log(player.Key + " " + player.Value);
        }
    }

    void RenderPlayer(KeyValuePair<string, string> playerNamePlayerPosition, bool deleteFromScene = false)
    {
        if (isServer)
        {
            if (deleteFromScene)
            {
                RpcHidePlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
            else
            {
                RpcRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
        }

        if (isLocalPlayer)
        {
            if (deleteFromScene)
            {
                CmdHidePlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
            else
            {
                CmdRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
        }
    }

    //void RenderPlayer(KeyValuePair<string, string> playerNamePlayerPosition)
    //{    

    //    if (isServer)
    //    {          
    //        RpcRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
    //    }

    //    if (isLocalPlayer)
    //    {
    //        CmdRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
    //    }
    //}

    [ClientRpc]
    public void RpcRenderPlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    [Command]
    public void CmdRenderPlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }


    [ClientRpc]
    public void RpcHidePlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    [Command]
    public void CmdHidePlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }


    public void OnTakeBike()
    {
        RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab), true);
        UIActualizer actualizer = new UIActualizer();
        actualizer.OnTakeBike(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab));
        RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab));
    }


}
