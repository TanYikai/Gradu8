using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// .UI for ui related

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text hiScoreText;
    public Text jellyScoreText;
    public Image[] lifepoints;

    public AudioSource deathSound;

    public float scoreCount;
    public float hiScoreCount;
    public int jellyScoreCount;
    public int lifepoint;

    public float pointsPerSecond;
	public int powerupMultiplier;

    public bool scoreIncreasing;
	public bool scorePowerup;

    public GameObject damageEffect;

    public float nextLevelScore;
    public GameObject nextLevel;

    // Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("HighScore"))
		{
			hiScoreCount = PlayerPrefs.GetFloat("HighScore");
		}
		//powerupMultiplier = 4;
	}
	
	// Update is called once per frame
	void Update () {

        if (FindObjectOfType<GameManager>().tut == false)
        {
            if (scoreIncreasing)
            {
                if (scorePowerup)
                    scoreCount += pointsPerSecond * Time.deltaTime * powerupMultiplier;
                else
                    scoreCount += pointsPerSecond * Time.deltaTime;
            }

            if (scoreCount > hiScoreCount)
            {
                hiScoreCount = scoreCount;
                PlayerPrefs.SetFloat("HighScore", hiScoreCount);//saves hiScore into computer
            }

            scoreText.text = "score : " + Mathf.Round(scoreCount);
            hiScoreText.text = "High Score : " + Mathf.Round(hiScoreCount);            

            if (jellyScoreCount >= nextLevelScore)
            {
                nextLevel.SetActive(true);
            }
        }

        jellyScoreText.text = jellyScoreCount.ToString();

    }

    public void AddScore(int pointsToAdd)
    {
		if(scorePowerup)
        {
			pointsToAdd = pointsToAdd * powerupMultiplier;
        }
        scoreCount += pointsToAdd;
    }

    public void JellyScore(int scoreAmount)
    {
        jellyScoreCount += scoreAmount;
        
    }

    public void BombDeduct(int bombAmount)
    {
        jellyScoreCount -= bombAmount;
    }

    public void LoseLife()
    {
        lifepoint--;

        Handheld.Vibrate();

        if (lifepoint == 0)
        {
            if (FindObjectOfType<GameManager>().tut == false)
            {
                FindObjectOfType<GameManager>().DeathScene();
                deathSound.Play();
            }
        }

        //for tutorial purposes
        if(lifepoint < 0)
        {
            lifepoint = 1;
        }

        if ((lifepoint - 1) >= 0)
             lifepoints[lifepoint - 1].enabled = false;

        damageEffect.SetActive(true);
        StartCoroutine("Delay");
        FindObjectOfType<SoundManager>().DamageSound();
    }

    public void AddLife()
    {
        lifepoint++;

        if(lifepoint >= 4)
        {
            lifepoint = 4;
        }

        lifepoints[lifepoint - 2].enabled = true;
    }

    public void FullLife()
    {
        lifepoint = 4;

        for (int i = 0; i < lifepoint - 1; i++)
        {
            lifepoints[i].enabled = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        damageEffect.SetActive(false);

    }
}
