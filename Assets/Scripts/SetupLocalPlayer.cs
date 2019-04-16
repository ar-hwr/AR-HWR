﻿using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    //player prefabs are assigned in unity editor
    public GameObject Jungfernheide;
    public GameObject JakobKaiserPlatz;
    public GameObject SchlossCharlottenburg;
    public GameObject Schlossbruecke;
    public GameObject FlughafenTegel;

    [SyncVar]
    public string pName;

    [HideInInspector] public static Dictionary<string, string> pPositions = new Dictionary<string, string>();

    public Color pColor;


    //[SyncVar(hook = "OnChangePlayerList")]
    //[SyncVar]
    public static string players;

    public Text NameInfo;
    //public Text AllPlayers;
    private ShowHidePlayers showHide;


    // Start is called before the first frame update
    void Start()
    {
        Jungfernheide = GameObject.Find("Jungfernheide");
        JakobKaiserPlatz = GameObject.Find("Jakob Kaiser Platz");
        SchlossCharlottenburg = GameObject.Find("Schloss Charlottenburg");
        Schlossbruecke = GameObject.Find("Schlossbruecke");
        FlughafenTegel = GameObject.Find("Flughafen Tegel");


        showHide = new ShowHidePlayers();


        foreach (var player in pPositions)
        {
            #region Jungfernheide

            if (pPositions.ContainsValue("Jungfernheide"))
            {
                showHide.RenderPlayer(pColor,
                    thief: GameObject.Find(Jungfernheide.name + "/thief"),
                    policeBlue: GameObject.Find(Jungfernheide.name + "/police blue"),
                    policeGreen: GameObject.Find(Jungfernheide.name + "/police green"),
                    policeRed: GameObject.Find(Jungfernheide.name + "/police red"),
                    policeYellow: GameObject.Find(Jungfernheide.name + "/police yellow"));
            }
            else
            {
                showHide.HidePlayersByDefault(
                    thief: GameObject.Find(Jungfernheide.name + "/thief"),
                    policeBlue: GameObject.Find(Jungfernheide.name + "/police blue"),
                    policeGreen: GameObject.Find(Jungfernheide.name + "/police green"),
                    policeRed: GameObject.Find(Jungfernheide.name + "/police red"),
                    policeYellow: GameObject.Find(Jungfernheide.name + "/police yellow"));
            }

            #endregion

            #region JakobKaiserPlatz
            if (pPositions.ContainsValue("JakobKaiserPlatz"))
            {
                showHide.RenderPlayer(pColor,
                    thief: GameObject.Find(JakobKaiserPlatz.name + "/thief"),
                    policeBlue: GameObject.Find(JakobKaiserPlatz.name + "/police blue"),
                    policeGreen: GameObject.Find(JakobKaiserPlatz.name + "/police green"),
                    policeRed: GameObject.Find(JakobKaiserPlatz.name + "/police red"),
                    policeYellow: GameObject.Find(JakobKaiserPlatz.name + "/police yellow"));
            }
            else
            {
                showHide.HidePlayersByDefault(
                    thief: GameObject.Find(JakobKaiserPlatz.name + "/thief"),
                    policeBlue: GameObject.Find(JakobKaiserPlatz.name + "/police blue"),
                    policeGreen: GameObject.Find(JakobKaiserPlatz.name + "/police green"),
                    policeRed: GameObject.Find(JakobKaiserPlatz.name + "/police red"),
                    policeYellow: GameObject.Find(JakobKaiserPlatz.name + "/police yellow"));
            }

            #endregion

            #region SchlossCharlottenburg
            if (pPositions.ContainsValue("SchlossCharlottenburg"))
            {
                showHide.RenderPlayer(pColor,
                    thief: GameObject.Find(SchlossCharlottenburg.name + "/thief"),
                    policeBlue: GameObject.Find(SchlossCharlottenburg.name + "/police blue"),
                    policeGreen: GameObject.Find(SchlossCharlottenburg.name + "/police green"),
                    policeRed: GameObject.Find(SchlossCharlottenburg.name + "/police red"),
                    policeYellow: GameObject.Find(SchlossCharlottenburg.name + "/police yellow"));
            }
            else
            {
                showHide.HidePlayersByDefault(
                    thief: GameObject.Find(SchlossCharlottenburg.name + "/thief"),
                    policeBlue: GameObject.Find(SchlossCharlottenburg.name + "/police blue"),
                    policeGreen: GameObject.Find(SchlossCharlottenburg.name + "/police green"),
                    policeRed: GameObject.Find(SchlossCharlottenburg.name + "/police red"),
                    policeYellow: GameObject.Find(SchlossCharlottenburg.name + "/police yellow"));
            }

            #endregion

            #region    
            if (pPositions.ContainsValue("Schlossbruecke"))
            {
                showHide.RenderPlayer(pColor,
                    thief: GameObject.Find(Schlossbruecke.name + "/thief"),
                    policeBlue: GameObject.Find(Schlossbruecke.name + "/police blue"),
                    policeGreen: GameObject.Find(Schlossbruecke.name + "/police green"),
                    policeRed: GameObject.Find(Schlossbruecke.name + "/police red"),
                    policeYellow: GameObject.Find(Schlossbruecke.name + "/police yellow"));
            }
            else
            {
                showHide.HidePlayersByDefault(
                    thief: GameObject.Find(Schlossbruecke.name + "/thief"),
                    policeBlue: GameObject.Find(Schlossbruecke.name + "/police blue"),
                    policeGreen: GameObject.Find(Schlossbruecke.name + "/police green"),
                    policeRed: GameObject.Find(Schlossbruecke.name + "/police red"),
                    policeYellow: GameObject.Find(Schlossbruecke.name + "/police yellow"));
            }

            #endregion

            #region
            if (pPositions.ContainsValue("FlughafenTegel"))
            {
                showHide.RenderPlayer(pColor,
                    thief: GameObject.Find(FlughafenTegel.name + "/thief"),
                    policeBlue: GameObject.Find(FlughafenTegel.name + "/police blue"),
                    policeGreen: GameObject.Find(FlughafenTegel.name + "/police green"),
                    policeRed: GameObject.Find(FlughafenTegel.name + "/police red"),
                    policeYellow: GameObject.Find(FlughafenTegel.name + "/police yellow"));
            }
            else
            {
                showHide.HidePlayersByDefault(
                    thief: GameObject.Find(FlughafenTegel.name + "/thief"),
                    policeBlue: GameObject.Find(FlughafenTegel.name + "/police blue"),
                    policeGreen: GameObject.Find(FlughafenTegel.name + "/police green"),
                    policeRed: GameObject.Find(FlughafenTegel.name + "/police red"),
                    policeYellow: GameObject.Find(FlughafenTegel.name + "/police yellow"));
            }
#endregion

        }



        //AllPlayers = GameObject.Find("Players").GetComponent<Text>();
        Debug.Log("My name is " + pName + "and my positions list looks like this:");
        //NameInfo.text = pName;
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
