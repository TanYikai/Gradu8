using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {
    
    //this platformDestructionPoint is a variable and not an actual object
    private GameObject platformDestructionPoint;

	// Use this for initialization
	void Start () {
        //The find object needs to be exactly the same as the ones created under the main camera which has capital 'P'
       platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }
	
	// Update is called once per frame
	void Update () {
		
        if(transform.position.x < platformDestructionPoint.transform.position.x)
        {
            // /*before object pooling*/ this destroys any game object that this script is attached to: before Object pooling version
            // Destroy(gameObject);

            //object pooling version
            gameObject.SetActive(false);

        }

	}
}
