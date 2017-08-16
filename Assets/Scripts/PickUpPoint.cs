using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoint : MonoBehaviour {

    public int scoreToGive;

    private ScoreManager theScoreManager;

	// Use this for initialization
	void Start ()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();      
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            theScoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);//disable gameObject after being picked up

            FindObjectOfType<SoundManager>().CoinBling();
        }
    }
}
