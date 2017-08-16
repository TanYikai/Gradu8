using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string playGameLevel;

	public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
        //Application.LoadLevel (playGameLevel); //obsolete
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
