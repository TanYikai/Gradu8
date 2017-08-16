using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController thePlayer;

    private Vector3 lastPlayerPosition;
    private float distanceToMoveX;
    // public float distanceToMoveY;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        distanceToMoveX = thePlayer.transform.position.x - lastPlayerPosition.x;
        
        transform.position = new Vector3(transform.position.x + distanceToMoveX, transform.position.y, transform.position.z);
        
        /*
        if (thePlayer.transform.position.y >= 0)
        {
            distanceToMoveY = thePlayer.transform.position.y - lastPlayerPosition.y;
            transform.position = new Vector3(transform.position.x, transform.position.y + distanceToMoveY, transform.position.z);
        }
        */

        lastPlayerPosition = thePlayer.transform.position;     
           
    }

}
