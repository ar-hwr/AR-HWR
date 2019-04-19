using System;
using System.Collections;
using System.Collections.Generic;
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

    //[SyncVar(hook = "OnChangePlayerList")]
    //[SyncVar]
    [HideInInspector]
    public static string Players;


    // Start is called before the first frame update
    void Start()
    {
        foreach (var player in PlayerNamePlayerPosition)
        {
            RenderPlayer(player);
                //var playerToRender = GameObject.Find(player.Value + "/" + player.Key);
                //playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);                   
        }

        //NetworkServer.FindLocalObject();

        Debug.Log("My name is " + PlayerName + "and my positions list looks like this:"); 
        foreach (var pos in PlayerNamePlayerPosition)
        {
            Debug.Log(pos.Key + " " + pos.Value);
        }

        //OnChangePlayerName(PlayerName);
    }



    void RenderPlayer(KeyValuePair<string, string> playerNamePlayerPosition)
    {    

        if (isServer)
        {          
            RpcRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
        }

        if (isLocalPlayer)
        {
            CmdRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
        }
    }

    [ClientRpc]
    public void RpcRenderPlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Debug.Log("client rpc");
        Debug.Log(playerToRender.name + " " + playerToRender.GetComponent<Transform>().localScale.ToString());
    }

    [Command]
    public void CmdRenderPlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Debug.Log("command");
        Debug.Log(playerToRender.name + " " + playerToRender.GetComponent<Transform>().localScale.ToString());
    }


    //void OnChangePlayerName(string name)
    //{
    //    if (isServer)
    //    {
    //        RpcChangePlayerNames(name);
    //    }

    //    if (isLocalPlayer)
    //    {
    //        CmdChangePlayerNames(name);
    //    }

    //    NameInfo.text = PlayerName;
    //}

    //[Command]
    //public void CmdChangePlayerNames(string name)
    //{
    //    Players = Players + " " + name;

    //    Debug.Log("Cmd set Players to" + Players);
    //}

    //[ClientRpc]
    //public void RpcChangePlayerNames(string name)
    //{
    //    Players = Players + " " + name;

    //    Debug.Log("Rcp set Players to" + Players);
    //}


    //void OnChangePlayerList(string name)
    //{
    //    AllPlayers.text = Players;
    //}
}
