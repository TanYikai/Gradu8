using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;

//	public float powerupLength;

	private int powerupSelector;
	private PowerupManager thePowerupManager;

	public Sprite[] powerupSprites;

	// Use this for initialization
	void Start () {
		thePowerupManager = FindObjectOfType<PowerupManager>();
	}

	//Change the respective powerup colour respectively
	void Awake () {
		powerupSelector = Random.Range(0, 3);

		GetComponent<SpriteRenderer>().sprite = powerupSprites[powerupSelector];
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Player")
		{
			//thePowerupManager.ActivatePowerup(doublePoints, safeMode, powerupLength);
			//tell power up manager to increment correct count for powerups
			thePowerupManager.IncreasePowerup(powerupSelector);
			gameObject.SetActive(false);
            FindObjectOfType<SoundManager>().PowerupGrab();
		}
	}
}
