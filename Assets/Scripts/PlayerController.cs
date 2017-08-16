using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // To enable DualTouchControls

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;

	//the 3 player speeds
	public float slowMoveSpeed;
	public float normalMoveSpeed;
	public float fastMoveSpeed;

    public float jumpTime;
    private float jumpTimeCounter;

    private bool stoppedJumping;
    private bool canDoubleJump;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    
    //private Collider2D myCollider;
    private Animator myAnimator;
    
    public GameManager theGameManager;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        stoppedJumping = true;
    }
	
	// Update is called once per frame
	void Update () {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		//Check for speed changes
		if (CrossPlatformInputManager.GetButtonDown("SlowSpeed"))
		{
			moveSpeed = slowMoveSpeed;
		}
		else if (CrossPlatformInputManager.GetButtonDown("NormalSpeed"))
		{
			moveSpeed = normalMoveSpeed;
		}
		else if (CrossPlatformInputManager.GetButtonDown("FastSpeed"))
		{
			moveSpeed = fastMoveSpeed;
		}

        //Enable movement 
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        
        //Enabling jumping
        if(CrossPlatformInputManager.GetButtonDown("Jump")) // || Input.GetMouseButtonDown(0))
        {
            //Enable jump only when touching the platform
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                FindObjectOfType<SoundManager>().JumpSound();
            }
            //Enable double jump
            if(!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                FindObjectOfType<SoundManager>().JumpSound();
            }
        }

        if(CrossPlatformInputManager.GetButton("Jump") && !stoppedJumping) //if(Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0) && !stoppedJumping)
        {   
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        //Prevents multiple jumps in the air
       if(CrossPlatformInputManager.GetButtonUp("Jump")) //|| Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
        //reset jumpTimeCounter once player hits the ground
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }
        
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            theGameManager.DeathScene();
            FindObjectOfType<SoundManager>().DeathSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            FindObjectOfType<ScoreManager>().LoseLife();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "warning")
        {
            FindObjectOfType<SoundManager>().DragonRoar();

            FindObjectOfType<DeadlineController>().AddSpeed(3);
            StartCoroutine("Delay");
            

        }

        if (collision.gameObject.tag == "killbox")
        {
            theGameManager.DeathScene();
            FindObjectOfType<SoundManager>().DeathSound();

            FindObjectOfType<SoundManager>().DragonMute();
            FindObjectOfType<SoundManager>().DragonKill();       
            FindObjectOfType<DeadlineController>().killed = true;
            


        }

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
        FindObjectOfType<DeadlineController>().ReduceSpeed(2);
    }

    /*
    //if were to use TouchControls for jump
    public void Jump()
    {
        // GetComponent<Rigidbody2D>().AddForce(new Vector3(0, jumpForce, 0));

        //Enable jump only when touching the platform
        if (grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            stoppedJumping = false;
            jumpSound.Play();
        }
        //Enable double jump
        if (!grounded && canDoubleJump)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            jumpTimeCounter = jumpTime;
            stoppedJumping = false;
            canDoubleJump = false;
            jumpSound.Play();
        }


        if (!stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        //Prevents multiple jumps in the air

        jumpTimeCounter = 0;
        stoppedJumping = true;

        //reset jumpTimeCounter once player hits the ground
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }
    }
    */

}
