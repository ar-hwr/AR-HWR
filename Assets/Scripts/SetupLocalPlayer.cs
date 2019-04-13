using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{

    //player prefabs are assigned in unity editor
    public GameObject Thief;
    public GameObject PoliceBlue;
    public GameObject PoliceGreen;
    public GameObject PoliceRed;
    public GameObject PoliceYellow;


    [SyncVar]
    public string pName;

    [HideInInspector] public static Dictionary<string, string> pPositions = new Dictionary<string, string>();

    public Color pColor;


    //[SyncVar(hook = "OnChangePlayerList")]
    //[SyncVar]
    public static string players;

    public Text NameInfo;
    //public Text AllPlayers;



    // Start is called before the first frame update
    void Start()
    {

        ShowHidePlayers showHide = new ShowHidePlayers();
        showHide.RenderPlayer(pColor, thief: Thief, policeBlue: PoliceBlue, policeGreen: PoliceGreen, policeRed: PoliceRed, policeYellow: PoliceYellow);

        //AllPlayers = GameObject.Find("Players").GetComponent<Text>();
        Debug.Log(pName);
        NameInfo.text = pName;
        //AllPlayers.text = players;

        foreach (var pos in pPositions)
        {
            Debug.Log(pos.Key + " " + pos.Value);
        }

        //OnChangePlayerName(pName);

    }

    //void Update()
    //{
    //    Debug.Log(pName);
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

    //    NameInfo.text = pName;
    //}

    //[Command]
    //public void CmdChangePlayerNames(string name)
    //{
    //    players = players + " " + name;

    //    Debug.Log("Cmd set players to" + players);
    //}

    //[ClientRpc]
    //public void RpcChangePlayerNames(string name)
    //{
    //    players = players + " " + name;

    //    Debug.Log("Rcp set players to" + players);
    //}


    //void OnChangePlayerList(string name)
    //{
    //    AllPlayers.text = players;
    //}
}
