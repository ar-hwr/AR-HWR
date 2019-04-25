﻿using System;
using System.Collections.Generic;
using System.Linq;
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

    //referencing UI
    public Text SubwayInfo;
    public Text BikeInfo;
    public Text BusInfo;
    public Dropdown BusConnectionDropdown;
    public Dropdown BikeConnectionDropdown;
    public Dropdown SubwayConnectionDropdown;
    public Button TakeBus;
    public Button TakeBike;
    public Button TakeSubway;

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

        GetRequestHandler getRequestHandler = new GetRequestHandler();
        //StartCoroutine(getRequestHandler.GetText2(SubwayInfo, result => UpdateUI(result)));
        //StartCoroutine(getRequestHandler.GetText2(SubwayInfo, result => UpdateUI(result)));
        StartCoroutine(getRequestHandler.FetchResponseFromWeb(result => UpdateUI(result)));
    }


    //updating bus dropdown
    [ClientRpc]
    void RpcUpdateBusConnection(string option)
    {
        BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    }

    [Command]
    void CmdUpdateBusConnection(string option)
    {
        BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    }

    //updating bike dropdown
    [ClientRpc]
    void RpcUpdateBikeConnection(string option)
    {
        BikeConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    }

    [Command]
    void CmdUpdateBikeConnection(string option)
    {
        BikeConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    }

    //updating subway dropdown
    [ClientRpc]
    void RpcUpdateSubwayConnection(string option)
    {
        networkedUpdatedSubwayConnection(option);
    }

    [Command]
    void CmdUpdateSubwayConnection(string option)
    {
        networkedUpdatedSubwayConnection(option);
    }

    private void networkedUpdatedSubwayConnection(string option)
    {
        SubwayConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    }


    [ClientRpc]
    void RpcClearOptions(string nameOfDropdown)
    {
        networkedClearOptions(nameOfDropdown);
    }

    [Command]
    void CmdClearOptions(string nameOfDropdown)
    {
        networkedClearOptions(nameOfDropdown);
    }

    private void networkedClearOptions(string nameOfDropdown)
    {
        GameObject.Find(nameOfDropdown).GetComponent<Dropdown>().ClearOptions();
    }


    [ClientRpc]
    void RpcForceUpdate(string nameOfDropdown)
    {
        networedUpdate(nameOfDropdown);
    }

    [Command]
    void CmdForceUpdate(string nameOfDropdown)
    {
        networedUpdate(nameOfDropdown);
    }

    private void networedUpdate(string nameOfDropdown)
    {
        var dropdown = GameObject.Find(nameOfDropdown).GetComponent<Dropdown>();
        dropdown.value = 1;
        dropdown.value = 0;
    }


    void UpdateUI(Data data)
    {
        SubwayInfo.text = data.subway_tickets.ToString();
        BikeInfo.text = data.bike_tickets.ToString();
        BusInfo.text = data.bus_tickets.ToString();

        var listOfBikeStations = new List<string>();
        var listOfBusStations = new List<string>();
        var listOfSubwayStations = new List<string>();

        foreach (var connection in data.connections)
        {
            switch (connection.type)
            {
                case "bike":
                    listOfBikeStations.Add(connection.node_name);
                    break;
                case "subway":
                    listOfSubwayStations.Add(connection.node_name);
                    break;
                case "bus":
                    listOfBusStations.Add(connection.node_name);
                    break;
                default:
                    break;
            }
        }

        AddOptionsToDropdown(listOfBikeStations, BikeConnectionDropdown, TakeBike);
        AddOptionsToDropdown(listOfBusStations, BusConnectionDropdown, TakeBus);
        AddOptionsToDropdown(listOfSubwayStations, SubwayConnectionDropdown, TakeSubway);
    }


    private void AddOptionsToDropdown(List<string> stationListForSelectedVehicle, Dropdown dropDownForSelectedVehicle, Button takeVehicle)
    {
        //dropDownForSelectedVehicle.ClearOptions();
        //RpcClearOptions(dropDownForSelectedVehicle.name);

        if (stationListForSelectedVehicle.Count == 0)
        {
            takeVehicle.interactable = false;
        }
        else
        {
            foreach (var station in stationListForSelectedVehicle)
            {
                dropDownForSelectedVehicle.options.Add(new Dropdown.OptionData() { text = station });

            }

            //this switch from 1 to 0 is only to refresh the visual DdMenu
            dropDownForSelectedVehicle.value = 1;
            dropDownForSelectedVehicle.value = 0;
        }


        if (isServer)
        {
            //dropDownForSelectedVehicle.ClearOptions();
            //RpcClearOptions(dropDownForSelectedVehicle.name);

            if (stationListForSelectedVehicle.Count == 0)
            {
                takeVehicle.interactable = false;
            }
            else
            {
                foreach (var station in stationListForSelectedVehicle)
                {
                    dropDownForSelectedVehicle.options.Add(new Dropdown.OptionData() { text = station });
                    switch (dropDownForSelectedVehicle.name)
                    {
                        case "BusDropdown":
                            RpcUpdateBusConnection(station);
                            break;
                        case "BikeDropdown":
                            RpcUpdateBikeConnection(station);
                            break;
                        case "SubwayDropdown":
                            RpcUpdateSubwayConnection(station);
                            break;
                        default:
                            break;
                    }
                }

                //this switch from 1 to 0 is only to refresh the visual DdMenu
                dropDownForSelectedVehicle.value = 1;
                dropDownForSelectedVehicle.value = 0;

                RpcForceUpdate(dropDownForSelectedVehicle.name);
            }
        }

        if (isLocalPlayer)
        {
            //dropDownForSelectedVehicle.ClearOptions();
            //CmdClearOptions(dropDownForSelectedVehicle.name);

            if (stationListForSelectedVehicle.Count == 0)
            {
                takeVehicle.interactable = false;
            }
            else
            {
                foreach (var station in stationListForSelectedVehicle)
                {
                    dropDownForSelectedVehicle.options.Add(new Dropdown.OptionData() { text = station });
                    switch (dropDownForSelectedVehicle.name)
                    {
                        case "BusDropdown":
                            CmdUpdateBusConnection(station);
                            break;
                        case "BikeDropdown":
                            CmdUpdateBikeConnection(station);
                            break;
                        case "SubwayDropdown":
                            CmdUpdateSubwayConnection(station);
                            break;
                        default:
                            break;
                    }
                }

                //this switch from 1 to 0 is only to refresh the visual DdMenu
                dropDownForSelectedVehicle.value = 1;
                dropDownForSelectedVehicle.value = 0;

                CmdForceUpdate(dropDownForSelectedVehicle.name);
            }
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
        GameObject.Find(value + "/" + key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    [Command]
    public void CmdRenderPlayer(string key, string value)
    {
        GameObject.Find(value + "/" + key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }


    [ClientRpc]
    public void RpcHidePlayer(string key, string value)
    {
        GameObject.Find(value + "/" + key).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    [Command]
    public void CmdHidePlayer(string key, string value)
    {
        GameObject.Find(value + "/" + key).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
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


    public void OnTakeBus()
    {
        OnTakeVehicle("BusDropdown");
    }

    public void OnTakeBike()
    {
        OnTakeVehicle("BikeDropdown");
    }

    public void OnTakeSubway()
    {
        OnTakeVehicle("SubwayDropdown");
    }


    public void OnTakeVehicle(string nameOfDropdown)
    {
        ActualizeDictionary(SerializedDictionary);

        RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab), true);
        UIActualizer actualizer = new UIActualizer();
        actualizer.OnTakeVehicle(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab), nameOfDropdown);

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