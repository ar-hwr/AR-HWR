using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MatchData
{
    public byte id;
    public string name;
    public string state;
    public byte mr_x_id;
    public byte current_player_id;
    public byte rounds_left;
    public byte mr_x_last_node_id;
    public string mr_x_last_ticket;
    public List<PlayerInGame> players;
    public string mr_x_last_node_name;
    public string current_player_name;
}