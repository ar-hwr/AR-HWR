using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    [SyncVar]
    public string PlayerName;


    public static Dictionary<string, string> PlayerNamePlayerPosition = new Dictionary<string, string>();

    [SyncVar]//(hook = "ActualizeDictionary")]
    public string SerializedDictionary;

    [HideInInspector]
    public Color PlayerColor;

    [SyncVar]
    [HideInInspector]
    public string PlayerPrefab;

    //[SyncVar(hook = "OnChangePlayerList")]
    //[SyncVar]
    [HideInInspector]
    public static string Players;

    public Text BikeInfo;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(PlayerName);
        Debug.Log(SerializedDictionary);

        foreach (var player in PlayerNamePlayerPosition)
        {
            RenderPlayer(player);
            Debug.Log(player.Key + " " + player.Value);
        }
        UIActualizer uiActualizer = new UIActualizer();
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

    void ActualizeDictionary(string serializedDictionary)
    {
        if (isServer)
        {
            PlayerNamePlayerPosition.Clear();
            PlayerNamePlayerPosition = customDeserialize(serializedDictionary);
            RpcActualizeDictionary(serializedDictionary);
        }

        if (isLocalPlayer)
        {
            PlayerNamePlayerPosition.Clear();
            PlayerNamePlayerPosition = customDeserialize(serializedDictionary);
            CmdActualizeDictionary(serializedDictionary);
        }
    }

    [ClientRpc]
    public void RpcRenderPlayer(string key, string value)
    {
        try
        {
            BikeInfo.text = value + "/" + key;
            var playerToRender = GameObject.Find(value + "/" + key);
            //BikeInfo.text = string.Empty;
            //BikeInfo.text = "Rcp Hide Player found gameobject with name" + playerToRender.name;
            //playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        catch (Exception e)
        {
            BikeInfo.text = e.Message;
        }
    }

    [Command]
    public void CmdRenderPlayer(string key, string value)
    {
        //BikeInfo.text = "Cmd searching for gameobject " + value + "/" + key;
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }


    [ClientRpc]
    public void RpcHidePlayer(string key, string value)
    {
        BikeInfo.text = "Rcp searching for gameobject " + value + "/" + key;

        try
        {
            BikeInfo.text = value + "/" + key;
            var playerToRender = GameObject.Find(value + "/" + key);
            //BikeInfo.text = string.Empty;
            //BikeInfo.text = "Rcp Hide Player found gameobject with name" + playerToRender.name;
            //playerToRender.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        }
        catch (Exception e)
        {
            BikeInfo.text = e.Message;
        }

    }

    [Command]
    public void CmdHidePlayer(string key, string value)
    {
        var playerToRender = GameObject.Find(value + "/" + key);
        playerToRender.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    [Command]
    public void CmdActualizeDictionary(string serializedDict)
    {
        PlayerNamePlayerPosition.Clear();
        PlayerNamePlayerPosition = customDeserialize(serializedDict);
    }

    [ClientRpc]
    public void RpcActualizeDictionary(string serializedDict)
    {
        PlayerNamePlayerPosition.Clear();
        PlayerNamePlayerPosition = customDeserialize(serializedDict);
    }


    public void OnTakeBike()
    {
        ActualizeDictionary(SerializedDictionary);


        RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab), true);
        UIActualizer actualizer = new UIActualizer();
        actualizer.OnTakeBike(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab));

        SerializedDictionary = customSerialize(PlayerNamePlayerPosition);
        ActualizeDictionary(SerializedDictionary);
        RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab));
    }

    public string customSerialize(Dictionary<string, string> players)
    {
        string z = String.Empty;
        foreach (var player in players)
        {           
            z += player.Key + "," + player.Value + ";";
        }
        z = z.Remove(z.Length - 1);
        return (z);
    }

    public Dictionary<string, string> customDeserialize(string serializedDict)
    {
        var players = new Dictionary<string, string>();
        var list = serializedDict.Split(';');
        foreach (var l in list)
        {
            var item = l.Split(',');
            players.Add(item[0], item[1]);
        }
        return players;
    }
}
