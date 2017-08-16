using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GellyManager : MonoBehaviour {

    private List<Gelly> jellies = new List<Gelly>();
    public GameObject jellyPrefab;

    private float lastSpawn;
    private float deltaSpawn;
    public float minSpawn;
    public float maxSpawn;

    public float maxVertical;
    public float minVertical;
    public float maxHorizontal;
    public float minHorizontal;

    public Transform trail;
    public GameObject spawnPoint;
    public bool isPaused;
  
    //private const float REQUIRED_SLICEFORCE = 0.0f;
    //private Vector3 lastMousePos;
    private Collider2D[] jelliesCols;

    private void Start()
    {
        jelliesCols = new Collider2D[0];
        isPaused = false;
    }

    private void Update()
    {
        if (isPaused)
            return;

        deltaSpawn = Random.Range(minSpawn, maxSpawn);
        if (Time.time - lastSpawn > deltaSpawn)
        {
            Gelly j = GetJelly();

            j.LaunchJelly(Random.Range(minVertical, maxVertical), Random.Range(minHorizontal, maxHorizontal), spawnPoint.transform.position);
            lastSpawn = Time.time;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))// || CrossPlatformInputManager.GetButton("Fire1")) ---> under testing, two ways of controlling slash
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;
            trail.transform.position = pos;

            Collider2D[] thisFrameJelly = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Jelly"));
            // if((Input.mousePosition - lastMousePos).sqrMagnitude > REQUIRED_SLICEFORCE)
            //  {	
            foreach (Collider2D c2 in thisFrameJelly)
            {
                for (int i = 0; i < jelliesCols.Length; i++)
                {
					if (c2 == jelliesCols[i] && c2.GetComponent<Gelly>() != null)
                    {
                        c2.GetComponent<Gelly>().Slice();
                       
                    }
                }
            }
            // }
            //lastMousePos = Input.mousePosition;
            jelliesCols = thisFrameJelly;

        }
    }

    /*public void IncrementScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();
    }*/

    private Gelly GetJelly()
    {
        Gelly j = jellies.Find(x => !x.IsActive);

        if (j == null)
        {
            j = Instantiate(jellyPrefab).GetComponent<Gelly>();
            jellies.Add(j);
        }

        return j;
    }
}
