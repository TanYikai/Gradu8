using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour {

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    private int spawnNum;

	void Start () {
        InvokeRepeating("SpawnType", 0, 5.0f);          		
	}
	
	void SpawnType()
    {
        spawnNum = Random.Range(0, 5);

        if(spawnNum == 0)
        {
            spawn1.SetActive(true);
            spawn2.SetActive(false);
            spawn3.SetActive(false);
            spawn4.SetActive(false);
        }
        else if(spawnNum == 1)
        {
            spawn1.SetActive(false);
            spawn2.SetActive(true);
            spawn3.SetActive(false);
            spawn4.SetActive(false);
        }
        else if(spawnNum == 2)
        {
            spawn1.SetActive(false);
            spawn2.SetActive(false);
            spawn3.SetActive(true);
            spawn4.SetActive(false);
        }
        else if(spawnNum == 3)
        {
            spawn1.SetActive(false);
            spawn2.SetActive(false);
            spawn3.SetActive(false);
            spawn4.SetActive(true);
        }
        else if(spawnNum == 4)
        {
            spawn1.SetActive(false);
            spawn2.SetActive(false);
            spawn3.SetActive(false);
            spawn4.SetActive(false);
        }
    }
}
