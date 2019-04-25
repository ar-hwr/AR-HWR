using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActualizer : MonoBehaviour
{
    public void OnTakeVehicle(KeyValuePair<string, string> playerNamePlayerValue, string nameOfDropdown)
    {
        var nextStation = GameObject.Find(nameOfDropdown).GetComponent<Dropdown>();
        SetupLocalPlayer.PlayerNamePlayerPosition[playerNamePlayerValue.Key] = nextStation.captionText.text;

        Debug.Log("User wants to go to station " + nextStation.captionText.text);
    }
}
