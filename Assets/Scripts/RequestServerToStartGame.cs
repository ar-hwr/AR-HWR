using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataToStartGame
{
    public int countPlayers = 2;
    public int countRounds = 1;
    public string password = string.Empty;
    public bool hasPassword = false;
}

public class RequestServerToStartGame : MonoBehaviour
{

    public Button startGameButton;
    public Slider countPlayersSlider;
    public Slider countRoundsSlider;
    public InputField passwordInputField;


    DataToStartGame serverRequest = new DataToStartGame();

    private string serverURL = "https://jsonplaceholder.typicode.com/todos/1";

    // Start is called before the first frame update
    void Start()
    {

        startGameButton.onClick.AddListener(RequestServer);
    }

    void RequestServer()
    {  
        serverRequest.countPlayers = (int)countPlayersSlider.value;
        serverRequest.countRounds = (int)countRoundsSlider.value;
        serverRequest.password = passwordInputField.text;
        if (serverRequest.password != null && serverRequest.password != string.Empty)
        {
            serverRequest.hasPassword = true;
        }

        StartCoroutine(GetText(result => { UnityEngine.Debug.Log(result.ToString()); }));

    }


    public IEnumerator GetText(Action<Test> result)
    {
        string textToSendToServer = JsonConvert.SerializeObject(serverRequest);
        // user UnityWebRequest and not Core WebRequests when working with Unity
        UnityWebRequest request = UnityWebRequest.Post(serverURL, textToSendToServer);
        UnityEngine.Debug.Log(textToSendToServer);


        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            if (result != null)
            {
                Test test = JsonConvert.DeserializeObject<Test>(request.downloadHandler.text);
                result(test);
            }
        }
    }
}
