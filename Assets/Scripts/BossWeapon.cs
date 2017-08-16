using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour {

	public float baseMoveSpeed;
	public float currMoveSpeed;
	public Rigidbody2D myRigidBody;

	public PlayerController thePlayer;
	private float displacement;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerController>();
		//get initial x value displacement
		displacement = transform.position.x - thePlayer.transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		//the object will move at a constant speed towards the player, by changing displacement to the player at a rate corresponding
		//to delta time between frames
		displacement -= baseMoveSpeed * Time.deltaTime;
		gameObject.transform.position = new Vector3(thePlayer.transform.position.x + displacement,
										gameObject.transform.position.y, gameObject.transform.position.z);
		/*Basically the projectile moves at a constant speed towards the playe	r, equal to baseMoveSpeed
		currMoveSpeed = thePlayer.moveSpeed - baseMoveSpeed;
		myRigidBody.velocity = new Vector2(currMoveSpeed, myRigidBody.velocity.y);*/
		//boss weapons go left, so use the usual destruction script
	}

}
