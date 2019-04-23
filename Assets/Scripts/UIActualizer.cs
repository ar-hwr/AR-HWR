using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Sharing.SyncModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIActualizer : NetworkBehaviour
{

    public Text SubwayInfo;
    public Text BikeInfo;
    public Text BusInfo;
    public Dropdown BusConnectionDropdown;
    public Dropdown BikeConnectionDropdown;
    public Dropdown SubwayConnectionDropdown;
    public Button TakeBus;
    public Button TakeBike;
    public Button TakeSubway;

    private SetupLocalPlayer setupLocalPlayer = new SetupLocalPlayer();


    // Start is called before the first frame update
    void Start()
    {
        //GetRequestHandler getRequestHandler = new GetRequestHandler();
        //StartCoroutine(getRequestHandler.GetText2(result => UpdateUI(result)));

        //if (isServer)
        //{
        //    RpcClearOptions(SubwayConnectionDropdown.name);
        //    BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = "neue Option" });
        //    RpcUpdateBusConnection("neue Option");
        //}

        //if (isLocalPlayer)
        //{
        //    CmdClearOptions(SubwayConnectionDropdown.name);
        //    BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = "neue Option" });
        //    CmdUpdateBusConnection("neue Option");
        //}
    }


    ////updating bus dropdown
    //[ClientRpc]
    //void RpcUpdateBusConnection(string option)
    //{
    //    BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    //}

    //[Command]
    //void CmdUpdateBusConnection(string option)
    //{
    //    BusConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    //}

    ////updating bike dropdown
    //[ClientRpc]
    //void RpcUpdateBikeConnection(string option)
    //{
    //    BikeConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    //}

    //[Command]
    //void CmdUpdateBikeConnection(string option)
    //{
    //    BikeConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    //}

    ////updating subway dropdown
    //[ClientRpc]
    //void RpcUpdateSubwayConnection(string option)
    //{
    //    SubwayConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    //}

    //[Command]
    //void CmdUpdateSubwayConnection(string option)
    //{
    //    SubwayConnectionDropdown.options.Add(new Dropdown.OptionData() { text = option });
    //}


    //[ClientRpc]
    //void RpcClearOptions(string nameOfDropdown)
    //{
    //    GameObject.Find(nameOfDropdown).GetComponent<Dropdown>().ClearOptions();
    //}

    //[Command]
    //void CmdClearOptions(string nameOfDropdown)
    //{
    //    GameObject.Find(nameOfDropdown).GetComponent<Dropdown>().ClearOptions();
    //}

    //[ClientRpc]
    //void RpcForceUpdate(string nameOfDropdown)
    //{
    //    var dropdown = GameObject.Find(nameOfDropdown).GetComponent<Dropdown>();
    //    dropdown.value = 1;
    //    dropdown.value = 0;
    //}

    //[Command]
    //void CmdForceUpdate(string nameOfDropdown)
    //{
    //    var dropdown = GameObject.Find(nameOfDropdown).GetComponent<Dropdown>();
    //    dropdown.value = 1;
    //    dropdown.value = 0;
    //}


    //void UpdateUI(Data data)
    //{
    //    SubwayInfo.text = data.subway_tickets.ToString();
    //    BikeInfo.text = data.bike_tickets.ToString();
    //    BusInfo.text = data.bus_tickets.ToString();

    //    var listOfBikeStations = new List<string>();
    //    var listOfBusStations = new List<string>();
    //    var listOfSubwayStations = new List<string>();

    //    foreach (var connection in data.connections)
    //    {
    //        switch (connection.type)
    //        {
    //            case "bike":
    //                listOfBikeStations.Add(connection.node_name);
    //                break;
    //            case "subway":
    //                listOfSubwayStations.Add(connection.node_name);
    //                break;
    //            case "bus":
    //                listOfBusStations.Add(connection.node_name);
    //                break;
    //            default:
    //                break;
    //        }

    //    }

    //    AddOptionsToDropdown(listOfBikeStations, BikeConnectionDropdown, TakeBike);
    //    AddOptionsToDropdown(listOfBusStations, BusConnectionDropdown, TakeBus);
    //    AddOptionsToDropdown(listOfSubwayStations, SubwayConnectionDropdown, TakeSubway);
    //}


    //private void AddOptionsToDropdown(List<string> stationListForSelectedVehicle, Dropdown dropDownForSelectedVehicle, Button takeVehicle)
    //{
    //    if (isServer)
    //    {
    //        dropDownForSelectedVehicle.ClearOptions();
    //        RpcClearOptions(dropDownForSelectedVehicle.name);

    //        if (stationListForSelectedVehicle.Count == 0)
    //        {
    //            takeVehicle.interactable = false;
    //        }
    //        else
    //        {
    //            foreach (var station in stationListForSelectedVehicle)
    //            {
    //                dropDownForSelectedVehicle.options.Add(new Dropdown.OptionData() { text = station });
    //                switch (dropDownForSelectedVehicle.name.ToLower())
    //                {
    //                    case "busdropdown":
    //                        RpcUpdateBusConnection(station);
    //                        break;
    //                    case "bikedropdown":
    //                        RpcUpdateBikeConnection(station);
    //                        break;
    //                    case "subwaydropdown":
    //                        RpcUpdateSubwayConnection(station);
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }

    //            //this switch from 1 to 0 is only to refresh the visual DdMenu
    //            dropDownForSelectedVehicle.value = 1;
    //            dropDownForSelectedVehicle.value = 0;

    //            RpcForceUpdate(dropDownForSelectedVehicle.name);
    //        }
    //    }

    //    if (isLocalPlayer)
    //    {

    //        dropDownForSelectedVehicle.ClearOptions();
    //        CmdClearOptions(dropDownForSelectedVehicle.name);

    //        if (stationListForSelectedVehicle.Count == 0)
    //        {
    //            takeVehicle.interactable = false;
    //        }
    //        else
    //        {
    //            foreach (var station in stationListForSelectedVehicle)
    //            {
    //                dropDownForSelectedVehicle.options.Add(new Dropdown.OptionData() { text = station });
    //                switch (dropDownForSelectedVehicle.name.ToLower())
    //                {
    //                    case "busdropdown":
    //                        CmdUpdateBusConnection(station);
    //                        break;
    //                    case "bikedropdown":
    //                        CmdUpdateBikeConnection(station);
    //                        break;
    //                    case "subwaydropdown":
    //                        CmdUpdateSubwayConnection(station);
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }

    //            //this switch from 1 to 0 is only to refresh the visual DdMenu
    //            dropDownForSelectedVehicle.value = 1;
    //            dropDownForSelectedVehicle.value = 0;

    //            CmdForceUpdate(dropDownForSelectedVehicle.name);
    //        }

    //    }
            
    //}



    public void OnTakeBus()
    {
        var nextStation = BusConnectionDropdown.captionText.text;

        SetupLocalPlayer.PlayerNamePlayerPosition[setupLocalPlayer.PlayerName] = nextStation;
        Debug.Log("User wants to go to station " + BusConnectionDropdown.captionText.text);

    }

    public void OnTakeBike(KeyValuePair<string, string> playerNamePlayerValue)
    {
        var nextStation = GameObject.Find("BikeDropdown").GetComponent<Dropdown>();
        SetupLocalPlayer.PlayerNamePlayerPosition[playerNamePlayerValue.Key] = nextStation.captionText.text;

        Debug.Log("User wants to go to station " + nextStation.captionText.text);
    }

    public void OnTakeSubway()
    {
        var nextStation = SubwayConnectionDropdown.captionText.text;
        SetupLocalPlayer.PlayerNamePlayerPosition[setupLocalPlayer.PlayerName] = nextStation;
        Debug.Log("User wants to go to station " + SubwayConnectionDropdown.captionText.text);
    }
}
