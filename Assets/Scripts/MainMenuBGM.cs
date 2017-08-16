using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBGM : MonoBehaviour {

    public AudioSource bgm;
	
	void Start () {
        if (bgm.isPlaying)
        {
            bgm.Stop();
            bgm.Play();
        }
        else
        {
            bgm.Play();
        }
	}
	
}
