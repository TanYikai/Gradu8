using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour {

	public float baseMoveSpeed;
	public float currMoveSpeed;
	public Rigidbody2D myRigidBody;

	public PlayerController thePlayer;
	public GameObject platformGenerationPoint;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerController>();
		platformGenerationPoint = GameObject.Find("PlatformGenerationPoint");
	}
	
	// Update is called once per frame
	void Update () {
		//Basically the projectile moves at a constant speed relative to the player, equal to baseMoveSpeed
		currMoveSpeed = thePlayer.moveSpeed + baseMoveSpeed;
		myRigidBody.velocity = new Vector2(currMoveSpeed, myRigidBody.velocity.y);

		//Since weapons go right, we destroy them when we reach platform creator (ahead of player)
		//Instead of the usual destruction script
		if (transform.position.x > platformGenerationPoint.transform.position.x)
		{
			gameObject.SetActive(false);
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (gameObject.tag == "antiplatform")
		{
			if (other.gameObject.tag == "platform")
			{
				other.gameObject.SetActive(false);
                FindObjectOfType<SoundManager>().PlatformCrushSound();
		
			}
		}
	}
}
