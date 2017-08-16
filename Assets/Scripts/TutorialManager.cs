using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject page0;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject page5;
    public GameObject page6;
    public GameObject page7;

    public GameObject lifePanel;
    public GameObject weapon;
    public GameObject jellyScore;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;

    public void Awake()
    {
        FindObjectOfType<GameManager>().tut = true;
    }

    public void Page1()
    {
        page0.SetActive(false);
        page1.SetActive(true);
        page2.SetActive(false);
        page2.SetActive(false);
        page2.SetActive(false);
        page2.SetActive(false);
        page2.SetActive(false);

        lifePanel.SetActive(false);
        weapon.SetActive(false);
        jellyScore.SetActive(false);

        spawn1.SetActive(false);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
        spawn4.SetActive(false);
        spawn5.SetActive(false);
        spawn6.SetActive(false);
    }

    public void Page2()
    {
        page0.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(true);
        page3.SetActive(false);
        page4.SetActive(false);
        page5.SetActive(false);
        page6.SetActive(false);
        page7.SetActive(false);

        lifePanel.SetActive(false);
        weapon.SetActive(false);
        jellyScore.SetActive(false);

        spawn1.SetActive(false);
        spawn2.SetActive(true);
        spawn3.SetActive(true);
        spawn4.SetActive(true);
        spawn5.SetActive(true);
        spawn6.SetActive(true);
    }

    public void Page3()
    {
        page0.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(true);
        page4.SetActive(false);
        page5.SetActive(false);
        page6.SetActive(false);
        page7.SetActive(false);

        lifePanel.SetActive(false);
        weapon.SetActive(false);
        jellyScore.SetActive(false);

        spawn1.SetActive(false);
        spawn2.SetActive(true);
        spawn3.SetActive(true);
        spawn4.SetActive(true);
        spawn5.SetActive(true);
        spawn6.SetActive(true);
    }

    public void Page4()
    {
        page0.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(true);
        page5.SetActive(false);
        page6.SetActive(false);
        page7.SetActive(false);

        lifePanel.SetActive(true);
        weapon.SetActive(true);
        jellyScore.SetActive(true);

        spawn1.SetActive(true);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
        spawn4.SetActive(false);
        spawn5.SetActive(false);
        spawn6.SetActive(false);
    }

    public void Page5()
    {
        page0.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
        page5.SetActive(true);
        page6.SetActive(false);
        page7.SetActive(false);

        lifePanel.SetActive(true);
        weapon.SetActive(true);
        jellyScore.SetActive(true);

        spawn1.SetActive(false);
        spawn2.SetActive(true);
        spawn3.SetActive(true);
        spawn4.SetActive(true);
        spawn5.SetActive(false);
        spawn6.SetActive(false);
    }

    public void Page6()
    {
        page0.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
        page5.SetActive(false);
        page6.SetActive(true);
        page7.SetActive(false);

        lifePanel.SetActive(true);
        weapon.SetActive(true);
        jellyScore.SetActive(true);

        spawn1.SetActive(false);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
        spawn4.SetActive(false);
        spawn5.SetActive(true);
        spawn6.SetActive(false);
    }

    public void Page7()
    {
        page0.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
        page5.SetActive(false);
        page6.SetActive(false);
        page7.SetActive(true);

        lifePanel.SetActive(true);
        weapon.SetActive(true);
        jellyScore.SetActive(true);

        spawn1.SetActive(false);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
        spawn4.SetActive(false);
        spawn5.SetActive(false);
        spawn6.SetActive(true);
    }
}
