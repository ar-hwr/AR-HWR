using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartViewing : MonoBehaviour
{
    public TMP_InputField InputGameName;
    public GameObject HeadingSubPanel;
    public GameObject PopUpPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnViewButtonClicked()
    {
        if (InputGameName.text != null && InputGameName.text != String.Empty)
        {
            RenderPlayers.gameName = InputGameName.text;

            GetRequestHandler getRequestHandler = new GetRequestHandler();
            StartCoroutine(getRequestHandler.FetchResponseFromWeb(
                InputGameName.text,
                result => TryStartViewing(result)));          
        }
    }

    private void TryStartViewing(MatchData matchData)
    {
        if (matchData.state == "404")
        {
            HeadingSubPanel.SetActive(false);
            PopUpPanel.SetActive(true);
        }
        else
        {
            HeadingSubPanel.SetActive(false);
        }    
    }

    public void OnConfirmButtonClicked()
    {
        PopUpPanel.SetActive(false);
        HeadingSubPanel.SetActive(true);
        InputGameName.text = String.Empty;
    }

}
