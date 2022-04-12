using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TankPlacement : MonoBehaviour
{
    // Unser Tank Element das wir platzieren wollen
    public GameObject tankPrefab;
    // Das interne Tank Objekt, welches wir nutzen um zu schauen, ob bereits ein Tank 
    // an diesem Spot im Spiel ist oder nicht.
    private GameObject tank;
    private Vector3 PositionSpawnort;
    GameManager gameManager;
    public int AnzUpgradesBogen = 0;
    public int AnzUpgradesCanon = 0;
    int cost;
    private bool Aktiv = false;

    /*private void OnMouseOver()
    {
        FindObjectOfType<DefStats>().nameLable.enabled = true;
    }*/
    private void Awake()
    {
        FindObjectOfType<GameManager>().aktuellesLevel = 1;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
      /*  //es muss zuerst ein Tank platziert werden, ohne Upgrade-Abfrage, sonst Error
        if(einmaligTankPlatzieren == false)
        {
            if (CoinsOverCost())
                FindObjectOfType<TankData>().UpgradeAnzeige.SetActive(true);
            else
                FindObjectOfType<TankData>().UpgradeAnzeige.SetActive(false);
        }*/
        
    }
    private void OnMouseEnter()
    {
        
        if(GameObject.FindGameObjectsWithTag("Bogen") != null)
        {
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Bogen").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("Bogen")[i].transform.GetChild(6).gameObject.SetActive(true);
            }
            
        }
    }
    private void OnMouseExit()
    {
        if (GameObject.FindGameObjectsWithTag("Bogen") != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Bogen").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("Bogen")[i].transform.GetChild(6).gameObject.SetActive(false);
            }

        }
    }
    // Sobald wir den Mausbutton loslassen
    private void OnMouseDown()
    {
        PositionSpawnort = transform.position;
       // PositionSpawnort.x -= 0.5f;
         PositionSpawnort.y += .8f;
        // Wenn wir einen Tank platzieren können
        if (CanPlaceTank())
        {
            cost = tankPrefab.GetComponent<TankData>().currentLevel.cost;
            // Fügen wir ihn in Spiel ein.
            if (gameManager.GetCoins() >= cost)
            {
                    tank = Instantiate(tankPrefab, PositionSpawnort, Quaternion.identity);
                gameManager.SetCoins(gameManager.GetCoins() - cost);
            }
            else
            {
                ;
            }
            
        }
        else if (CanUpgradeTank())
        {
            int cost = tank.GetComponent<TankData>().GetNextLevel().cost;
                if (gameManager.GetCoins() >= cost)
                {
                    //2x Upgraden der Bogentürme möglich
                    if (FindObjectOfType<TankData>().GetTankLevel() == FindObjectOfType<TankData>().level[0] ||
                    FindObjectOfType<TankData>().GetTankLevel() == FindObjectOfType<TankData>().level[1] ||
                    FindObjectOfType<TankData>().GetTankLevel() == FindObjectOfType<TankData>().level[2])
                         AnzUpgradesBogen++;
                    //2x Upgraden der Kanonentürme möglich
                    else if (FindObjectOfType<TankData>().GetTankLevel() == FindObjectOfType<TankData>().level[3] ||
                    FindObjectOfType<TankData>().GetTankLevel() == FindObjectOfType<TankData>().level[4] ||
                    FindObjectOfType<TankData>().GetTankLevel() == FindObjectOfType<TankData>().level[5])
                    AnzUpgradesCanon++;

                if (AnzUpgradesBogen < 3 && AnzUpgradesCanon < 3)
                {
                    tank.GetComponent<TankData>().IncreaseLevel();
                    gameManager.SetCoins(gameManager.GetCoins() - tank.GetComponent<TankData>()
                        .currentLevel.cost);

                }
            }
          
           
            
        }
           
        
    }

    // Damit überprüfen wir ob an diesem TankSpot bereits ein Tank ist oder nicht.
    private bool CanPlaceTank()
    {
        if (tank == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Damit überprüfen wir, ob wir unseren Tank upgraden können.
    private bool CanUpgradeTank()
    {
        // Das geht natürlich nur, wenn ein Tank überhaupt existiert.
        if (tank != null)
        {
            // Wir holen uns das TankDataScript
            TankData data = tank.GetComponent<TankData>();
            // Dann holen wir uns das nächste Level für den Tank
            TankLevel nextLevel = data.GetNextLevel();
            // Wenn das nicht leer ist, geben wir true zurück.
            if (nextLevel != null)
            {
                return true;
            }
        }
        // Wenn nicht, dann false.
        return false;
    }


}
