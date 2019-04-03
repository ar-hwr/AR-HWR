using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    [SyncVar] public string pName = "player";

    public Text NameInfo;

    // Start is called before the first frame update
    void Start()
    {
        NameInfo.text = pName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
