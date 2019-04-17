using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TakeSubway : NetworkBehaviour
{

    void Start()
    {
        takeSubwayButton.onClick.AddListener(ClientTakeSubway);
    }

    //[SyncVar(hook = "OnChangeSubwayCount")]
    [SyncVar]
    private int subwayMoves = 4;
    public Text subwayInfo;
    public Button takeSubwayButton;


    ////reduces the number of moves on server
    //[Command]
    //void CmdTakeSubway()
    //{
    //    subwayMoves--;
    //}

    //redirects the request to the server
    void ClientTakeSubway()
    {
        //CmdTakeSubway(); 
        subwayMoves--;
        subwayInfo.text = subwayMoves.ToString();
    }

    ////actualizes the Players view
    //void OnChangeSubwayCount(int subwayMoves)
    //{
    //    subwayInfo.text = subwayMoves.ToString();
    //}
}
