using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyGun : MonoBehaviour {

    public GameObject jellyPrefab;

	void Start () {
        InvokeRepeating("ReleaseJelly", 1f, 1f);
	}
	
	void ReleaseJelly() {
        GameObject jelly = (GameObject)Instantiate(jellyPrefab, this.transform.position, Quaternion.identity);
        jelly.GetComponent<Rigidbody2D>().AddForce(this.transform.forward * 1000);
    }
}
