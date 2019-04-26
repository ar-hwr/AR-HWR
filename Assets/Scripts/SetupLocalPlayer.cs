using System;
using System.Collections.Generic;
using System.Linq;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Player
{
    public string Name;
    public Color Color;
    public string Prefab;
    public string Position;
}

public class SetupLocalPlayer : NetworkBehaviour
{
    public List<Player> PlayerList = new List<Player>();

    [SyncVar]
    public string WhoseTurnIsIt;

    public static Dictionary<string, string> PlayerNamePlayerPosition = new Dictionary<string, string>();
    public List<string> PlayerPrefabs = new List<string>();

    [SyncVar(hook="ActualizeDictionary")]
    public string SerializedDictionary;

    [HideInInspector]
    public Color PlayerColor;

    [SyncVar]
    [HideInInspector]
    public string PlayerPrefab;

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

    // Start is called before the first frame update
    void Start()
    {
        foreach (var player in PlayerList)
        {
            Debug.Log("\nCreated player with these attributes");
            Debug.Log(player.Name);   
            Debug.Log(player.Prefab); 
            Debug.Log(player.Color);
            Debug.Log(player.Position);   


            RenderPlayer(player);
        }


        //Debug.Log(SerializedDictionary);

        //PlayerNamePlayerPosition.Clear();
        //PlayerNamePlayerPosition = customDeserialize(SerializedDictionary);



        //WhoseTurnIsIt = PlayerPrefabs.First();
        //TurnInfo.text = WhoseTurnIsIt;
        
        GetRequestHandler getRequestHandler = new GetRequestHandler();
        StartCoroutine(getRequestHandler.FetchResponseFromWeb(result => UpdateUI(result)));
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
        FillDropdown(stationListForSelectedVehicle, takeVehicle, dropDownForSelectedVehicle);        
    }


    private void FillDropdown(List<string> stationListForSelectedVehicle, Button takeVehicle, Dropdown dropDownForSelectedVehicle)
    {
        dropDownForSelectedVehicle.ClearOptions();

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
                        BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = station });
                        break;
                    case "BikeDropdown":
                        BikeConnectionDropdown.options.Add(new Dropdown.OptionData() { text = station });
                        break;
                    case "SubwayDropdown":
                        SubwayConnectionDropdown.options.Add(new Dropdown.OptionData() { text = station });
                        break;
                    default:
                        break;
                }
            }

            //this switch from 1 to 0 is only to refresh the visual DdMenu
            dropDownForSelectedVehicle.value = 1;
            dropDownForSelectedVehicle.value = 0;
        }
    }

    void RenderPlayer(Player player, bool deleteFromScene = false)
    {
        //first position then prefab
        if (isServer)
        {
            if (deleteFromScene)
            {
                GameObject.Find(player.Position + "/" + player.Prefab).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                RpcHidePlayer(player);
            }
            else
            {
                GameObject.Find(player.Position + "/" + player.Prefab).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                RpcRenderPlayer(player);
            }
        }


        if (isLocalPlayer)
        {
            if (deleteFromScene)
            {
                GameObject.Find(player.Position + "/" + player.Prefab).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                CmdRenderPlayer(player);
            }
            else
            {
                GameObject.Find(player.Position + "/" + player.Prefab).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                CmdHidePlayer(player);
            }
        }
    }

    void ActualizeDictionary(string serializedDictionary)
    {
        PlayerNamePlayerPosition.Clear();
        PlayerNamePlayerPosition = customDeserialize(serializedDictionary);
    }

    [ClientRpc]
    public void RpcRenderPlayer(Player player)
    {
        GameObject.Find(player.Position + "/" + player.Prefab).GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    [Command]
    public void CmdRenderPlayer(Player player)
    {
        RpcRenderPlayer(player);
    }

    [ClientRpc]
    public void RpcHidePlayer(Player player)
    {
        GameObject.Find(player.Position + "/" + player.Prefab).GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    [Command]
    public void CmdHidePlayer(Player player)
    {
        RpcHidePlayer(player);
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
        GetNextElement(WhoseTurnIsIt);

        //RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab), true);
        UIActualizer actualizer = new UIActualizer();
        actualizer.OnTakeVehicle(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab), nameOfDropdown);

        SerializedDictionary = customSerialize(PlayerNamePlayerPosition);
        ActualizeDictionary(SerializedDictionary);
        //RenderPlayer(PlayerNamePlayerPosition.SingleOrDefault(x => x.Key == PlayerPrefab));
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
        TurnInfo.text = WhoseTurnIsIt;
    }   
}