using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int i = 0;

    public Transform SpawnOrt;
    public GameObject[] Gegner;
    public GameManager gameManager;
    public bool startCou = true;
    public bool ersteMalSpawnen = true;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        //StartCoroutine("SpawnGegner");
    }

    private void FixedUpdate()
    {
       /* if(gameManager.AnzSpawns == gameManager.AnzSpawnsMax)
        {
            StopCoroutine("SpawnGegner");
        }*/

        if (gameManager.startroutine == true || gameManager.AnzSpawns == gameManager.AnzSpawnsMax)
        {
            if (startCou )
            {
                StartCoroutine("SpawnGegner");
                
            }
            else
            {
                StopCoroutine("SpawnGegner");
                
            }
            startCou = false;
            //Nur einmal ausführen
            if (i < 1)
            {
                gameManager.startroutine = false;
                i++;
            }
            
        }

    }

    private IEnumerator SpawnGegner()
    {
        while (true)
        {
            //Timer, wie lange zufällig gewartet wird, bis Methode neu ausgeführt wird.
            if (ersteMalSpawnen)
            {
                yield return new WaitForSeconds(gameManager.Spawnzeit + 5);
                ersteMalSpawnen = false;
            }
            else
            {
                yield return new WaitForSeconds(gameManager.Spawnzeit);
                GameObject go = Gegner[Random.Range(0, Gegner.Length)];
                //Transform von Spawnort

                Vector3 SpawnPosition = SpawnOrt.transform.position;
                Quaternion SpawnRotation = Quaternion.Euler(0f, 180f, 0f);

                float rand = Random.Range(0, 100);

                //Gegner Spawnwn
                Debug.Log(SpawnPosition);
                GameObject gespawnderGegner = Instantiate(go, SpawnPosition, SpawnRotation);
                gameManager.AnzSpawns++;
                Debug.Log(gameManager.AnzSpawns);
            }

            
        }

    }
}
