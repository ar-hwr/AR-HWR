using Prototype.NetworkLobby;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class NetworkLobbyHook : LobbyHook
{
    private List<string> Stations = new List<string>
    {
        "Jungfernheide",
        "JakobKaiserPlatz",
        "SchlossCharlottenburg",
        "Schlossbruecke",
        "Flughafen Tegel"
    };


    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer,
        GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetupLocalPlayer localPlayer = gamePlayer.GetComponent<SetupLocalPlayer>();


        localPlayer.pName = lobby.playerName;
        localPlayer.pColor = lobby.playerColor;
        //localPlayer.players = localPlayer.players + " " + lobby.playerName;
        //Debug.Log(localPlayer.players);
        SetupLocalPlayer.players += lobby.playerName;
        Debug.Log("Players is " + SetupLocalPlayer.players);

        Random random = new Random();
        int index = random.Next(0, 4);
        SetupLocalPlayer.pPositions.Add(lobby.playerName, Stations[index]);

    }
}

