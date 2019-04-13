using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePlayers : MonoBehaviour
{   
    public GameObject Thief;
    public GameObject PoliceBlue;
    public GameObject PoliceGreen;
    public GameObject PoliceRed;
    public GameObject PoliceYellow;

    [HideInInspector] public Dictionary<GameObject, bool> PlayerBools = new Dictionary<GameObject, bool>();


    // Start is called before the first frame update
    void Start()
    {
       PlayerBools.Add(Thief, false);
       PlayerBools.Add(PoliceBlue, false);
       PlayerBools.Add(PoliceGreen, false);
       PlayerBools.Add(PoliceRed, true);
       PlayerBools.Add(PoliceYellow, false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in PlayerBools)
        {
            var renderer = player.Key.GetComponent<Renderer>();
            if (player.Value)
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }
    }
}
