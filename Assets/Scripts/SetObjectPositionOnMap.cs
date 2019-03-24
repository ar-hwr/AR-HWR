using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectPositionOnMap : MonoBehaviour
{
    public GameObject gameObject;
    private Vector3 position;
    private Vector3 positionOfParent;
    

    void Start()
    {
        // get position for the object from server
        // server is not able to tell positions at time of writing this 06/03/19
        // ~Jessica
        position = new Vector3(0, 0, 0);
        //Debug.Log("pos is at the begin " + gameObject.transform.position.x + " " + gameObject.transform.position.y + " " + gameObject.transform.position.z);
        positionOfParent = new Vector3(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y, gameObject.transform.parent.position.z);
  
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x != position.x ||
            gameObject.transform.position.y != position.y ||
            gameObject.transform.position.z != position.z)
        {
            positionOfParent = new Vector3(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y, gameObject.transform.parent.position.z);

            SetPosition(gameObject, new Vector3(0.01f, 0, 0));
            //Debug.Log("pos is now " + gameObject.transform.position.x);
        }
    }

    private void SetPosition(GameObject objectToMove, Vector3 positionFromServer)
    {
        if(objectToMove.transform.position.x < 1)
        {
            objectToMove.transform.position += positionFromServer;
        }
        else
        {
            objectToMove.transform.position = positionOfParent;
        }



    }
}
