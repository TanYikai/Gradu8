using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public string Level1;
    public string Level2;
    public string Level3;
    public string Level4;
    public string LevelBoss;
    public string LevelBonus;

    public void PlayLevel1()
    {
        FindObjectOfType<GameManager>().tut = false;
        SceneManager.LoadScene(Level1);       
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(Level2);
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene(Level3);
    }

    public void PlayLevel4()
    {
        SceneManager.LoadScene(Level4);
    }

    public void PlayLevelBoss()
    {
        SceneManager.LoadScene(LevelBoss);
    }

    public void PlayLevelBonus()
    {
        SceneManager.LoadScene(LevelBonus);
    }

}
