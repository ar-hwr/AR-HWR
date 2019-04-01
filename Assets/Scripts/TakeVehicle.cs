using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TakeVehicle : NetworkBehaviour
{

    [SyncVar]
    public int subwayMoves = 4;

    [SyncVar]
    public int bikeMoves = 4;

    [SyncVar]
    public int busMoves = 4;

    public Text subwayInfo;
    public Button takeSubwayButton;

    public Text busInfo;
    public Button takeBusButton;

    public Text bikeInfo;
    public Button takeBikeButton;

    // Start is called before the first frame update
    void Start()
    {
        subwayInfo.text = subwayMoves.ToString();
        busInfo.text = busMoves.ToString();
        bikeInfo.text = bikeMoves.ToString();
    }

    public void ClientTakeSubway()
    {
        subwayMoves--;
        subwayInfo.text = subwayMoves.ToString();
    }

    public void ClientTakeBus()
    {
        busMoves--;
        busInfo.text = busMoves.ToString();
    }

    public void ClientTakeBike()
    {
        bikeMoves--;
        bikeInfo.text = bikeMoves.ToString();
    }

}
