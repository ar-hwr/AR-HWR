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
                var playerToRender = GameObject.Find(player.Value + "/" + player.Key);
                playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);                   
        }


        Debug.Log("My name is " + PlayerName + "and my positions list looks like this:"); 
        foreach (var pos in PlayerNamePlayerPosition)
        {
            Debug.Log(pos.Key + " " + pos.Value);
        }

        //OnChangePlayerName(PlayerName);
    }

    //void Update()
    //{
    //    Debug.Log(PlayerName);
    //}



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
