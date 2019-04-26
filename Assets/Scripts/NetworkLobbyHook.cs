using Prototype.NetworkLobby;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class NetworkLobbyHook : LobbyHook
{
    /// <summary>
    /// Names of stations on the map, exactly spelled like in the unity scene
    /// </summary>
    private readonly List<string> Stations = new List<string>
   {
        "Alexanderplatz",
        "Am Volkspark",
        "Berliner Str.",
        "Brandenburger Tor",
        "Bundesplatz",
        "Checkpoint Charlie",
        "Europa Center",
        "Flughafen Tegel",
        "Frankfurter Allee",
        "Friedrichstraße",
        "Gesundbrunnen",
        "Grenzallee",
        "Großer Stern",
        "Grunewald Roseneck",
        "Hauptbahnhof",
        "Haus der Kulturen der Welt",
        "Herthastr.",
        "Hohenzollerndamm",
        "Hohenzollernplatz",
        "Jakob Kaiser Platz",
        "Jungfernheide",
        "Karl-Marx-Platz",
        "Landsberger Allee",
        "Märkisches Museum",
        "Messe Süd",
        "Messedamm ZOB",
        "Ostbahnhof",
        "Planetenstraße",
        "Platz der Luftbrücke",
        "Platz der Vereinten Nationen",
        "Potsdamer Platz",
        "Rathaus Neukölln",
        "Rathaus Tiergarten",
        "Rosa-Luxemburg-Platz",
        "Schloss Bellevue",
        "Schloss Charlottenburg",
        "Schlossbrücke",
        "Südkreuz",
        "Tempelhof",
        "Theodor-Heuss-Platz",
        "Tiergarten",
        "Treptower Park",
        "Walther Schreiber Platz",
        "Westhafen",
        "Wilmersdorfer Str.",
        "Wittenbergplatz",
        "Zoologischer Garten"
    };

    /// <summary>
    /// Mapping of colors and player prefabs
    /// </summary>
    private readonly Dictionary<Color, string> playerColorPlayerName = new Dictionary<Color, string>
    {
        { Color.black, "thief" },
        { Color.blue, "police blue" },
        { Color.green, "police green" },
        { Color.red, "police red" },
        { Color.yellow, "police yellow" }
    };

    /// <summary>
    /// when the scene is loaded for the Players, attributes need to be passed from lobby scene to main scene
    /// </summary>
    /// <param name="manager">network manager handling networked objects</param>
    /// <param name="lobbyPlayer">player prefab in scene lobby</param>
    /// <param name="gamePlayer">player prefab in scene main</param>
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer,
        GameObject gamePlayer)
    {
        //instantiating a player prefab for both the lobby scene and the main scene
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetupLocalPlayer localPlayer = gamePlayer.GetComponent<SetupLocalPlayer>();

        //mapping attributes from scene to scene
        //localPlayer.PlayerName = lobby.playerName;
        localPlayer.PlayerColor = lobby.playerColor;
        localPlayer.PlayerPrefab = playerColorPlayerName[lobby.playerColor];
   
        //creating and logging a list of Players that joined the game
        SetupLocalPlayer.Players += lobby.playerName;
        Debug.Log("Players is " + SetupLocalPlayer.Players);

        //randomly choosing a station to spawn player and notifying the main scene
        Random random = new Random();
        int stationIndex = random.Next(0, Stations.Count -1);
        SetupLocalPlayer.PlayerNamePlayerPosition.Add(playerColorPlayerName[localPlayer.PlayerColor], Stations[stationIndex]);


        localPlayer.SerializedDictionary = localPlayer.customSerialize(SetupLocalPlayer.PlayerNamePlayerPosition);


        Debug.Log("Station is " + Stations[stationIndex]);

        Player createdPlayer = new Player();
        createdPlayer.Name = lobby.playerName;
        createdPlayer.Color = lobby.playerColor;
        createdPlayer.Prefab = playerColorPlayerName[lobby.playerColor];
        Random rand = new Random();
        int stIndex = random.Next(0, Stations.Count - 1);
        createdPlayer.Position = Stations[stIndex];
        localPlayer.PlayerList.Add(createdPlayer);
    }
}

