using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TakeVehicle : NetworkBehaviour
{
    [SyncVar(hook = "OnSubwayChange")]
    public int subwayMoves = 4;

    public int bikeMoves = 4;

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
        if (isServer)
            RpcDecrementSubwayMoves(1);

        if (isLocalPlayer)
            CmdDecrementSubwayMoves(1);
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

    [Command]
    public void CmdDecrementSubwayMoves(int number)
    {
        subwayMoves -= number;
    }

    [ClientRpc]
    public void RpcDecrementSubwayMoves(int number)
    {
        subwayMoves -= number;
    }

    void OnSubwayChange(int number)
    {
        subwayInfo.text = number.ToString();
    }

}
