using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public GameObject userId;
    public GameObject id;
    public GameObject title;
    public GameObject completed;
        

    // Start is called before the first frame update
    void Start()
    {
        // request server asynchronously -> using a coroutine
        GetRequestHandler requestHandler = new GetRequestHandler();
        StartCoroutine(requestHandler.GetText(result =>
        {
            userId.GetComponent<Text>().text = result.userId;
            id.GetComponent<Text>().text = result.id.ToString();
            title.GetComponent<Text>().text = result.title.ToString();
            completed.GetComponent<Text>().text = result.completed.ToString();
            //UnityEngine.Debug.Log(result.userId + ' ' + result.id + ' ' + result.title + ' ' + result.completed);
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
