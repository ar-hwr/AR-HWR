﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePlayers : MonoBehaviour
{
    [HideInInspector] public Dictionary<GameObject, bool> PlayerBools = new Dictionary<GameObject, bool>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderPlayer(Color pColor, GameObject thief, GameObject policeBlue, GameObject policeGreen, GameObject policeRed, GameObject policeYellow)
    {
        Debug.Log("called renderPlayers with param " + pColor);
        Dictionary<Color, GameObject> pColors = new Dictionary<Color, GameObject>
        {
            { Color.black, thief },
            { Color.blue, policeBlue },
            { Color.green, policeGreen },
            { Color.red, policeRed },
            { Color.yellow, policeYellow }
        };

        GameObject player = pColors[pColor];

        PlayerBools.Add(thief, false);
        PlayerBools.Add(policeBlue, false);
        PlayerBools.Add(policeGreen, false);
        PlayerBools.Add(policeRed, false);
        PlayerBools.Add(policeYellow, false);

        switch (player.name)
        {
            case "thief":
                PlayerBools[thief] = true;
                break;
            case "police blue":
                PlayerBools[policeBlue] = true;
                break;
            case "police green":
                PlayerBools[policeGreen] = true;
                break;
            case "police red":
                PlayerBools[policeRed] = true;
                break;
            case "police yellow":
                PlayerBools[policeYellow] = true;
                break;
            default:
                break;
        }



        foreach (var pb in PlayerBools)
        {
            var renderer = pb.Key.GetComponent<Renderer>();
            if (pb.Value)
            {
                renderer.enabled = true;
                Debug.Log("rendering player " + player.name);
            }
            else
            {
                renderer.enabled = false;
            }
        }
    }
}