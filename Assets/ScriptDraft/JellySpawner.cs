using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellySpawner : MonoBehaviour {

    public GameObject[] jellyPrefab; 

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnJelly()); 
	}

    IEnumerator SpawnJelly()
    {
        while(true)
        {
            GameObject jel = Instantiate(jellyPrefab[Random.Range(0, jellyPrefab.Length)]);
            Rigidbody2D temp = jel.GetComponent<Rigidbody2D>();

            temp.velocity = new Vector3(0f, 5f, .5f);

            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            jel.transform.position = pos;

            yield return new WaitForSeconds(1f);
        }
    }
	
}
