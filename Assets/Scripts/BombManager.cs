using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BombManager : MonoBehaviour {

    private List<Bomb> bombs = new List<Bomb>();
    public GameObject BombPrefab;

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

    private const float REQUIRED_SLICEFORCE = 2.0f;
    private Vector3 lastMousePos;
    private Collider2D[] BombCols;

    private void Start()
    {
        BombCols = new Collider2D[0];
        isPaused = false;
    }

    private void Update()
    {
        if (isPaused)
            return;

        deltaSpawn = Random.Range(minSpawn,maxSpawn);
        if (Time.time - lastSpawn > deltaSpawn)
        {
            Bomb b = GetBomb();

            b.LaunchBomb(Random.Range(minVertical, maxVertical), Random.Range(minHorizontal, maxHorizontal), spawnPoint.transform.position);
            lastSpawn = Time.time;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))// || CrossPlatformInputManager.GetButton("Fire1")) ---> under testing, two ways of controlling slash
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;
            trail.transform.position = pos;

            Collider2D[] thisFrameBomb = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Bomb"));
            if ((Input.mousePosition - lastMousePos).sqrMagnitude > REQUIRED_SLICEFORCE)
            {
                foreach (Collider2D c2 in thisFrameBomb)
                {
                    for (int i = 0; i < BombCols.Length; i++)
                    {
						if (c2 == BombCols[i] && c2.GetComponent<Bomb>() != null)
                        {
                            c2.GetComponent<Bomb>().Slice();
                        }
                    }
                }
            }
            lastMousePos = Input.mousePosition;
            BombCols = thisFrameBomb;

        }
    }

    private Bomb GetBomb()
    {
        Bomb b = bombs.Find(x => !x.IsActive);

        if (b == null)
        {
            b = Instantiate(BombPrefab).GetComponent<Bomb>();
            bombs.Add(b);
        }

        return b;
    }
}
