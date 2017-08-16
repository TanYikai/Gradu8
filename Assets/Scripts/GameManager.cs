using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform[] platformGenerators;
    private Vector3[] platformStartPoints;

    public PlayerController thePlayer;
    private Vector3 playerStartPoint;

    public GameObject theDeadline;
    private Vector3 deadlineStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;

    public DeathMenu theDeathScreen;

    public bool powerupReset;
    public bool tut;

    public GameObject creditsBar;

	// Use this for initialization
	void Start () {
		platformStartPoints = new Vector3[platformGenerators.Length];
       
        for (int i = 0; i < platformGenerators.Length; i++)
		{
			platformStartPoints[i] = platformGenerators[i].position;
		}
        playerStartPoint = thePlayer.transform.position;
        deadlineStartPoint = theDeadline.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();

        if (FindObjectOfType<GameManager>().tut == false)
            FindObjectOfType<PauseMenu>().LevelIntro();

        FindObjectOfType<SoundManager>().BGM();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DeathScene()
    {
        Time.timeScale = 0f;

        JellyManager[] j = FindObjectsOfType(typeof(JellyManager)) as JellyManager[];
        foreach (JellyManager jelly in j)
        {
            jelly.isPaused = true;
        }

        BlacklyManager[] bk = FindObjectsOfType(typeof(BlacklyManager)) as BlacklyManager[];
        foreach (BlacklyManager jelly in bk)
        {
            jelly.isPaused = true;
        }

        BellyManager[] bu = FindObjectsOfType(typeof(BellyManager)) as BellyManager[];
        foreach (BellyManager jelly in bu)
        {
            jelly.isPaused = true;
        }

        GellyManager[] g = FindObjectsOfType(typeof(GellyManager)) as GellyManager[];
        foreach (GellyManager jelly in g)
        {
            jelly.isPaused = true;
        }

        RellyManager[] r = FindObjectsOfType(typeof(RellyManager)) as RellyManager[];
        foreach (RellyManager jelly in r)
        {
            jelly.isPaused = true;
        }

        BombManager[] b = FindObjectsOfType(typeof(BombManager)) as BombManager[];
        foreach (BombManager bomb in b)
        {
            bomb.isPaused = true;
        }

        theScoreManager.scoreIncreasing = false;
        
        //stop AirWalk sound if player is killed during its activation,MUST put BEFORE deacivating player
        FindObjectOfType<WeaponManager>().AirWalkOff();

        thePlayer.gameObject.SetActive(false);

        theDeathScreen.gameObject.SetActive(true);

        FindObjectOfType<SoundManager>().BGMStop();

        //StartCoroutine("RestartGameCo");//calls the RestartGameCo() method
    }

    public void Reset()
    {
        theScoreManager.lifepoint = 4;
        for (int i = 0; i < theScoreManager.lifepoint - 1; i++)
        {
            theScoreManager.lifepoints[i].enabled = true;
        }

        Time.timeScale = 1f;

        JellyManager[] j = FindObjectsOfType(typeof(JellyManager)) as JellyManager[];
        foreach (JellyManager jelly in j)
        {
            jelly.isPaused = false;
        }

        BlacklyManager[] bk = FindObjectsOfType(typeof(BlacklyManager)) as BlacklyManager[];
        foreach (BlacklyManager jelly in bk)
        {
            jelly.isPaused = false;
        }

        BellyManager[] bu = FindObjectsOfType(typeof(BellyManager)) as BellyManager[];
        foreach (BellyManager jelly in bu)
        {
            jelly.isPaused = false;
        }

        GellyManager[] g = FindObjectsOfType(typeof(GellyManager)) as GellyManager[];
        foreach (GellyManager jelly in g)
        {
            jelly.isPaused = false;
        }

        RellyManager[] r = FindObjectsOfType(typeof(RellyManager)) as RellyManager[];
        foreach (RellyManager jelly in r)
        {
            jelly.isPaused = false;
        }

        BombManager[] b = FindObjectsOfType(typeof(BombManager)) as BombManager[];
        foreach (BombManager bomb in b)
        {
            bomb.isPaused = false;
        }

        theDeathScreen.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
		for (int i = 0; i < platformGenerators.Length; i++)
		{
			platformGenerators[i].position = platformStartPoints[i];
		}
        thePlayer.transform.position = playerStartPoint;
        theDeadline.transform.position = deadlineStartPoint;

		//if (theDeadline.tag == "deadline")
		//{
			theDeadline.GetComponent<DeadlineController>().ResetSpeed();
			FindObjectOfType<DeadlineController>().killed = false;
			FindObjectOfType<DeadlineController>().pause = false;
		//}

        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.jellyScoreCount = 0;
        theScoreManager.scoreIncreasing = true;
        theScoreManager.nextLevel.SetActive(false);

        FindObjectOfType<SoundManager>().BGM();

        powerupReset = true;
    }

    public void CreditsOn()
    {
        creditsBar.SetActive(true);
    }

    public void CreditsOff()
    {
        creditsBar.SetActive(false);
    }
    
    /* //Method to restart game
     public IEnumerator RestartGameCo()
     {
         theScoreManager.scoreIncreasing = false;
         thePlayer.gameObject.SetActive(false);//To make player invisible when it touches Catcher
         yield return new WaitForSeconds(0.5f);//sets a 0.5s delay before exe next line

         platformList = FindObjectsOfType<PlatformDestroyer>();
         for(int i = 0; i < platformList.Length; i++)
         {
             platformList[i].gameObject.SetActive(false);
         }

         thePlayer.transform.position = playerStartPoint;
         platformGenerator.position = platformStartPoint;
         thePlayer.gameObject.SetActive(true);

         theScoreManager.scoreCount = 0;
         theScoreManager.scoreIncreasing = true;
     } */
}
