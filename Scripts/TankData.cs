using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Durch die Verwendung von [System.Serializable]  können wir das Tanklevel nun direkt in Unity bearbeiten.
[System.Serializable]
// Diese Klasse ist dafür da, Tanklevel einfach zu handhaben.
// In dem wir setzen, welche Kosten das einzelne Level hat und wie es aussieht.
public class TankLevel
{
   
    public int cost;
    public GameObject display;
    public GameObject bullet;
    public float fireRate;
    public float AnzUprades = 0;

}


public class TankData : MonoBehaviour
{
    // Die Liste aller Level unserer Tanks, z.B. 3 Tanks in unserem Fall
    public List<TankLevel> level;
    // Das aktuelle Level das wir aktiv haben. Muss nicht public sein, aber
    // wir können es dadurch leichter beobachten.
    public TankLevel currentLevel;

   
    private void Awake()
    {
        level[0].cost = 100;
        level[1].cost = 200;
        level[2].cost = 300;
        level[3].cost = 100;
        level[4].cost = 225;
        level[5].cost = 350;
        // for (int i = 0; i < FindObjectOfType<GameManager>().PreisText.Length; i++)
        //{
        for (int i = 0; i < FindObjectOfType<GameManager>().PreisText.Length; i++)
        {
            if (FindObjectOfType<GameManager>().PreisText[i] == null)
                continue;
            FindObjectOfType<GameManager>().PreisText[i].GetComponent<Text>().text = level[i].cost.ToString();
        }
           
       // }
    }

    private void Update()
    {
        PanelDrehen();
        ZeigeUpgradeSymbol();
    }
    // Sobald das Objekt aktiviert wird (also in unserem Fall der Tank) wird die OnEnable Methode aufgerufen
    private void OnEnable()
    {

        
        if (FindObjectOfType<GameManager>().aktuellesLevel == 1)
       {
            currentLevel = level[0];
            
       }
        else if(FindObjectOfType<GameManager>().aktuellesLevel == 2)
        {
            currentLevel = level[3];

        }
        else
        {
            currentLevel = level[0];
        }

        // Das Level wird auf 0 gesetzt (also tank_1)
        // Und der SetTankLevel Methode übergeben, die sich letzten Endes um das Aktivieren
        // und deaktivieren der entsprechenden Tankslevels kümmert.

        ColliderÄndern();
        
        SetTankLevel(currentLevel);
    }

    // Damit holen wir uns das aktuelle Level.
    public TankLevel GetTankLevel()
    {
        return currentLevel;
    }

    // Damit setzen wir das Level unseres aktuellen Tanks auf das übergebene Level.
    public void SetTankLevel(TankLevel tanklevel)
    {
        // Wir setzen das übergebene Level als aktuelles Level.
        currentLevel = tanklevel;

        // Wir setzen den CurrentLevelIndex  (z.B. für Tank_1 ist der Index 0, für Tank_2 ist der Index 1 etc.
        int currentLevelIndex = level.IndexOf(currentLevel);
        // Das level das Wir anzeigen wollen wird geholt aus der Liste "level" welche alle unsere Tanks
        // mit ihren entsprechenden Kosten und Prefabs besitzt.
        GameObject levelDisplay = level[currentLevelIndex].display;

        // Mit der Forschleife aktivieren wir nur den Tankprefab, für den dieser Tank das entsprechende Level hat.
        for (int i = 0; i < level.Count; i++)
        {
            if (levelDisplay != null)
            {
                
               if (i == currentLevelIndex)
                {
                    //Damit aktivieren wir einen Tank
                    level[i].display.SetActive(true);
                }
                
                else
                {
                    // Damit deaktivieren wir einen Tank. Er ist also nicht mehr im Spiel sichtbar und verwendbar.
                    level[i].display.SetActive(false);
                }
            }
        }

    }

    // HIermit erhöhen wir das Level
    public void IncreaseLevel()
    {
        // wir brauchen den aktuellen Levelindex
        int currentLevelIndex = level.IndexOf(currentLevel);
        // Und überprüfen ob wir noch nicht das Maximum überschritten haben
        if(currentLevelIndex < level.Count - 1)
        {
            
            // Das level setzen wir nun in unser Current level
            currentLevel = level[currentLevelIndex + 1];

            // Und übergeben es der SetTankLevel Methode
            SetTankLevel(currentLevel);
        }
    }

    // Damit holen wir uns das nächste Level als TankLevel Datentyp zurück.
    public TankLevel GetNextLevel()
    {
        // wir brauchen den aktuellen Levelindex
        int currentLevelIndex = level.IndexOf(currentLevel);
        // Und das maximale Level, das aus anzahl an Tanks -1 besteht.
        int maxLevelIndex = level.Count - 1;
        // Wenn das Maximallevel noch nicht erreicht ist
        if (currentLevelIndex < maxLevelIndex)
        {   // erhöhe es um 1 und gib das TankLevel Objekt zurück
            return level[currentLevelIndex + 1];
        }
        else
        {
            // Anonsten gib nichts zurück
            return null;
        }
    }

    public void ColliderÄndern()
    {
        if (GetTankLevel() == level[3] || GetTankLevel() == level[4] || GetTankLevel() == level[5])
        {
            
            this.GetComponent<BoxCollider>().size = new Vector3(8f, 1f, 8f);
            this.transform.GetChild(6).localScale = new Vector3(8f, .2f, 8f);

        }
        else
        {
            this.GetComponent<BoxCollider>().size = new Vector3(12f, 1f, 12f);
            this.transform.GetChild(6).localScale = new Vector3(12f, .2f, 12f);
        }
    }

    public void ZeigeUpgradeSymbol()
    {
        for(int i = 0; i < level.Count - 1; i++)
        {
            int[] aktKosten = { 200, 300, 0, 225, 350 };
            if (i == 2)
                continue;
            if (GetTankLevel() == level[i])
            {
                if (FindObjectOfType<GameManager>().coins >= aktKosten[i])
                    this.transform.GetChild(i).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                else
                    this.transform.GetChild(i).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }

        
    }
    
    public void PanelDrehen()
    {
        //BogenturmLvl1
        for(int i = 0; i < this.transform.childCount - 1; i++)
        {
            this.transform.GetChild(i).GetChild(1).GetChild(0).gameObject.transform.rotation = Quaternion.Euler(FindObjectOfType<KameraRotator>().yAngle,
           FindObjectOfType<KameraRotator>().xAngle, 0);
        }

    }

}
