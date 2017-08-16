using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public ObjectPooler weaponPool;
	private PlayerController thePlayer;

    public GameObject fullScreen;
    public GameObject timeHack;
    public GameObject fullHealthEffect;

    public Animator airWalkAnimation;

    // Use this for initialization
    void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Postcond: weapon created, set to player position and activated
	public void FireWeapon()
	{
		//Move the weapon to the position of the player and make it active when 'fired'
		GameObject newWeapon = weaponPool.GetPooledObject();
		//shift weapon roughly to center of player
		newWeapon.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y + 1.0f, thePlayer.transform.position.z);
		newWeapon.SetActive(true);
	}

    public void FullAttack()
    {
        fullScreen.SetActive(true);
        fullHealthEffect.SetActive(true);
        StartCoroutine("Delay");

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        fullScreen.SetActive(false);
        fullHealthEffect.SetActive(false);
    }

    public void TimeHack()
    {
        timeHack.SetActive(true);
        FindObjectOfType<SoundManager>().TimeHackStart();
        Time.timeScale = 0f;

        StartCoroutine("TimeStop");
    }

    IEnumerator TimeStop()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        Time.timeScale = 1f;
        timeHack.SetActive(false);
        FindObjectOfType<SoundManager>().TimeHackStop();
    }

    public void AirWalkOn()
    {
        FindObjectOfType<PlayerController>().groundCheckRadius = 10.0f;
        airWalkAnimation.SetBool("AirWalk", true);
        FindObjectOfType<SoundManager>().AirWalkStart();
    }

    public void AirWalkOff()
    {
        FindObjectOfType<PlayerController>().groundCheckRadius = 0.1f;
        airWalkAnimation.SetBool("AirWalk", false);
        FindObjectOfType<SoundManager>().AirWalkStop();
    }
}
