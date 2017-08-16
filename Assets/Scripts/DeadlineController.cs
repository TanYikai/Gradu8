using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlineController : MonoBehaviour {

    public float moveSpeed;
    public float initialSpeed;

    public bool killed;
    public bool pause;

    private Rigidbody2D myRigidbody;
    private Animator thisAnimator;

    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();

        thisAnimator.SetBool("Killed", killed);
        thisAnimator.SetBool("Pause", pause);
    }
	
	void Update () {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        thisAnimator.SetBool("Killed", killed);
        thisAnimator.SetBool("Pause", pause);
    }

    public void AddSpeed(int speed)
    {
         moveSpeed = moveSpeed + speed;
    }

    public void ReduceSpeed(int speed)
    {
        moveSpeed = moveSpeed - speed;
    }

    public void ResetSpeed()
    {
        moveSpeed = initialSpeed;
    }

}
