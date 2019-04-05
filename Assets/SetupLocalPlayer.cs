using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    public string pName;

    //[SyncVar(hook = "OnChangePlayerList")]
    public string players;

    public Text NameInfo;
    public Text AllPlayers;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(pName);
        NameInfo.text = pName;

;        //OnChangePlayerName(pName);
    }

   
    void OnChangePlayerName(string name)
    { 
        if (isServer)
        {
            RpcChangePlayerNames(name);
        }

        if (isLocalPlayer)
        {
            CmdChangePlayerNames(name);
        }

        NameInfo.text = pName;
    }

    [Command]
    public void CmdChangePlayerNames(string name)
    {
        players = players + " " + name;

        Debug.Log("Cmd set players to" + players);
    }

    [ClientRpc]
    public void RpcChangePlayerNames(string name)
    {
        players = players + " " + name;

        Debug.Log("Rcp set players to" + players);
    }


    void OnChangePlayerList(string name)
    {
        AllPlayers.text = players;
    }
}
