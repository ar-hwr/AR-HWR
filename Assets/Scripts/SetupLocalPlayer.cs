using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    [SyncVar]
    public int bikes = 5;


    [SyncVar(hook = "OnSubwayChange")]
    public int subwayMoves = 4;

    [SyncVar]
    public string PlayerName;

    [SyncVar]
    public String WhoseTurnIsIt;

    public static Dictionary<string, string> PlayerNamePlayerPosition = new Dictionary<string, string>();
    public List<string> PlayerPrefabs = new List<string>();

    [SyncVar(hook="ActualizeDictionary")]
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
    public Text BusInfo;
    public Text BikeInfo;
    public Text TurnInfo;
    public Dropdown BusConnectionDropdown;
    public Dropdown BikeConnectionDropdown;
    public Dropdown SubwayConnectionDropdown;
    public Button TakeBus;
    public Button TakeBike;
    public Button TakeSubway;
    public Button Anzeige;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerName);
        Debug.Log(SerializedDictionary);

        PlayerNamePlayerPosition.Clear();
        PlayerNamePlayerPosition = customDeserialize(SerializedDictionary);

        foreach (var player in PlayerNamePlayerPosition)
        {
            RenderPlayer(player);
            Debug.Log(player.Key + " " + player.Value);

            PlayerPrefabs.Add(player.Key);

        }


        WhoseTurnIsIt = PlayerPrefabs.First();
        

        GetRequestHandler getRequestHandler = new GetRequestHandler();
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
        //BikeInfo.text = data.bike_tickets.ToString();
        BusInfo.text = data.bus_tickets.ToString();

        var listOfBikeStations = new List<string>();
        var listOfBusStations = new List<string>();
        var listOfSubwayStations = new List<string>();

        foreach (var connection in data.connections)
        {
            switch (connection.type)
            {
                case "bike":
                    listOfBikeStations.Add(connection.destination_name);
                    break;
                case "subway":
                    listOfSubwayStations.Add(connection.destination_name);
                    break;
                case "bus":
                    listOfBusStations.Add(connection.destination_name);
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
                GameObject.Find(playerNamePlayerPosition.Value + "/" + playerNamePlayerPosition.Key).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                RpcHidePlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
            else
            {
                GameObject.Find(playerNamePlayerPosition.Value + "/" + playerNamePlayerPosition.Key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                RpcRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
        }

        if (isLocalPlayer)
        {
            if (deleteFromScene)
            {
                GameObject.Find(playerNamePlayerPosition.Value + "/" + playerNamePlayerPosition.Key).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                CmdHidePlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
            else
            {
                GameObject.Find(playerNamePlayerPosition.Value + "/" + playerNamePlayerPosition.Key).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                CmdRenderPlayer(playerNamePlayerPosition.Key, playerNamePlayerPosition.Value);
            }
        }
    }

    void ActualizeDictionary(string serializedDictionary)
    {
        PlayerNamePlayerPosition.Clear();
        PlayerNamePlayerPosition = customDeserialize(serializedDictionary);

        if (isServer)
        {
            RpcActualizeDictionary(serializedDictionary);
        }

        if (isLocalPlayer)
        {
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
        RpcRenderPlayer(key, value);
    }


    [ClientRpc]
    public void RpcHidePlayer(string key, string value)
    {
        GameObject.Find(value + "/" + key).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    [Command]
    public void CmdHidePlayer(string key, string value)
    {
        RpcHidePlayer(key, value);
    }

    [Command]
    public void CmdActualizeDictionary(string serializedDict)
    {
        RpcActualizeDictionary(serializedDict);
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
        CalculateNextPlayer();

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

    private void GetNextElement(string playerPrefab)
    {
        var index = PlayerPrefabs.FindIndex(a => a == playerPrefab);

        if ((index > PlayerPrefabs.Count - 1) || (index < 0))
            throw new Exception("Invalid index");

        else if (index == PlayerPrefabs.Count - 1)
            index = 0;

        else
            index++;



        WhoseTurnIsIt = PlayerPrefabs[index];


        if (isServer)
        {
            RpcSetWhoseTurn(PlayerPrefabs[index]);
        }

        if (isLocalPlayer)
        {
            CmdSetWhoseTurn(PlayerPrefabs[index]);
        }



        TurnInfo.text = WhoseTurnIsIt;
    }

    [Command]
    private void CmdSetWhoseTurn(string who)
    {
        RpcSetWhoseTurn(who);
    }

    [ClientRpc]
    private void RpcSetWhoseTurn(string who)
    {
        WhoseTurnIsIt = who;
    }


    [Command]
    private void CmdGetNextElement(string player)
    {
        GetNextElement(player);
        RpcGetNextElement(player);
    }

    [ClientRpc]
    private void RpcGetNextElement(string player)
    {
       GetNextElement(player);
    }


    private void DecrementSubwayMoves(int number)
    {
        subwayMoves -= number;
    }

    [Command]
    private void CmdDecrementSubwayMoves(int number)
    {
        RpcDecrementSubwayMoves(number);
    }

    [ClientRpc]
    private void RpcDecrementSubwayMoves(int number)
    {
        subwayMoves -= number;
    }

    private void OnSubwayChange(int number)
    {
        if (isServer)
            SubwayInfo.text = number.ToString();

        if (isLocalPlayer)
            SubwayInfo.text = number.ToString();


        SubwayInfo.text = number.ToString();

        if (isServer)
        {
            SubwayInfo.text = number.ToString();
            RpcSetDebugText(number.ToString());
        }

        if (isLocalPlayer)
            CmdSetDebugText(number.ToString());

    }


    public void CalculateNextPlayer()
    {
        var WhoseTurnThisRound = WhoseTurnIsIt;

        if (isServer)
        {
            RpcGetNextElement(WhoseTurnThisRound);
        }


        if (isLocalPlayer)
        {
            GetNextElement(WhoseTurnThisRound);
            CmdGetNextElement(WhoseTurnThisRound);
        }

    }

    [Command]
    private void CmdSetDebugText(string text)
    {
        RpcSetDebugText(text);
    }

    [ClientRpc]
    private void RpcSetDebugText(string text)
    {
        SubwayInfo.text = text;
    }









    public void ClickTest()
    {
        bikes++;
        if (isServer)
        {
            BikeInfo.text = bikes.ToString();
            if (bikes % 2 == 0)
            {
                Anzeige.interactable = false;
            }
            else
            {
                Anzeige.interactable = true;
            }
            RpcPrintVar(bikes.ToString());
        }

        if (isLocalPlayer)
        {
            BikeInfo.text = bikes.ToString();
            if (bikes % 2 == 0)
            {
                Anzeige.interactable = false;
            }
            else
            {
                Anzeige.interactable = true;
            }
            CmdPrintVar(bikes.ToString());
        }
    }


    [Command]
    private void CmdPrintVar(string text)
    {
        BikeInfo.text = text;
        if (bikes % 2 == 0)
        {
            Anzeige.interactable = false;
        }
        else
        {
            Anzeige.interactable = true;
        }
        RpcPrintVar(text);
    }

    [ClientRpc]
    private void RpcPrintVar(string text)
    {
        bikes = Convert.ToInt32(text);
        BikeInfo.text = bikes.ToString();
        if (bikes % 2 == 0)
        {
            Anzeige.interactable = false;
        }
        else
        {
            Anzeige.interactable = true;
        }
    }

}