using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    // Die Geschwindigkeit unserer Kugel
    public float speed = 10;
    // Der Schaden den sie erzeugen kann
    public int damage;
    // Das Ziel der Kugel, also wo soll sie hinfliegen
    public GameObject target;
    // Die Startposition der Kugel
    public Vector3 startPosition;
    // Die Zielposition der Kugel
    public Vector3 targetPosition;

    // Der Abstand zwischen Start und Ziel
    private float distance;
    // Die Zeit in der die Kugel erzeugt wurde
    private float startTime;
    private bool getroffen = true;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        // Die Zeit in der die Kugel erzeugt wurde
        startTime = Time.time;
        // Der Abstand zwischen Start und Ziel
        distance = Vector3.Distance(startPosition, targetPosition);
        //source = this.GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
       
        //  Zeit die die Kugel bereits lebt
        float timeInterval = Time.time - startTime;
        // Die neue Position der Kugel, also wird sie jeden Frame ein bisschen verschoben
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed/distance);
        // Wenn die Kugel das Ziel erreicht hat
        if (gameObject.transform.position.Equals(targetPosition))
        {
            
            // Überprüfen wir ob das Ziel noch da ist
            if (target != null)
            {
                   
                // Wir holen uns die Healthbar, also das GameObject und davon die Transform Component
                Transform healthBarTransform = target.transform.GetChild(1).Find("HealthBar");
                // Von dieser holen wir uns das Healthbar Script
                Lebensleiste healthBar = healthBarTransform.gameObject.GetComponent<Lebensleiste>();
                // Darin verändern wir nun die aktuelle Lebensanzeige um -Schaden oder 0, jenachdem was größer ist
                healthBar.aktuellesLeben -= Mathf.Max(damage, 0);
                AudioManager.instance.enemyHit.Play();
                // Falls der Gegner nun weniger als 0 oder 0 Leben hat
                if (healthBar.aktuellesLeben <= 0)
                {
                    
                    // Zerstören wir den Gegner
                    Destroy(target);
                    FindObjectOfType<GameManager>().toteGegner++;
                    AudioManager.instance.EnemyDead.Play();
                    // TODO Sound abspielen

                    // Und erhöhen unsere Coins um 50
                    FindObjectOfType<GameManager>().SetCoins(FindObjectOfType<GameManager>().GetCoins() + 
                        FindObjectOfType<GameManager>().coinsProKill);
                    // GameManager.singleton.SetCoins(GameManager.singleton.GetCoins() + 50);
                }
                Destroy(gameObject);
            }

            // Natürlich zerstören wir auch die Kugel, sobald wir die Zielposition erreicht haben.
           
        }
        Destroy(gameObject, 0.7f);

    }
}
