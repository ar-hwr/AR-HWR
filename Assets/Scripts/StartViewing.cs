using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartViewing : MonoBehaviour
{
    public TMP_InputField InputGameName;
    public static GameObject HeadingSubPanel;
    public static GameObject PopUpPanel;
    public static GameObject InGamePanel;
    public static GameObject PoliceWonPanel;
    public static GameObject ThiefWonPanel;

    // Start is called before the first frame update
    void Start()
    {
        HeadingSubPanel = GameObject.Find("HeadingSubPanel");
        PopUpPanel = GameObject.Find("PopUpPanel");
        InGamePanel = GameObject.Find("InGamePanel");
        PoliceWonPanel = GameObject.Find("PoliceWonPanel");
        ThiefWonPanel = GameObject.Find("ThiefWonPanel");

        PopUpPanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        InGamePanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        PoliceWonPanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        ThiefWonPanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
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
            HeadingSubPanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            PopUpPanel.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            HeadingSubPanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            InGamePanel.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void OnPoliceWon()
    {

        InGamePanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        PoliceWonPanel.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void OnThiefWon()
    {
        InGamePanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        ThiefWonPanel.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void OnLeaveGame()
    {
        //SceneManager.LoadScene("Main");
        Application.Quit();
    }

    public void OnConfirmButtonClicked()
    {
        PopUpPanel.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        HeadingSubPanel.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        InputGameName.text = String.Empty;
    }

}
