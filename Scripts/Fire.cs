using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Fire : MonoBehaviour
{
    // Liste der Gegner die in Reichweite sind
    public List<GameObject> enemiesInRange;

    // Zeit wann der letzte Schuss abgefeuert wurde.
    private float lastShotTime;
    // Das Tankdata Script welches wir brauchen um seine Methoden zu nutzen
    private TankData tankData;
    public GameObject grandChild1;
    public GameObject grandChild2;
   
    // Start is called before the first frame update
    void Start()
    {

        
        
        // Wir setzen die Liste auf eine neue Liste, damit sie nicht null ist
        enemiesInRange = new List<GameObject>();
        // Wir setzen den LastShotTime auf die Zeit der erzeugung des Tanks
        lastShotTime = Time.time;
        // Wir holen uns das TankDataScript
        tankData = gameObject.GetComponentInChildren<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        // Hiermit setzen wir das Ziel erstmal auf null also auf leer.
        GameObject target = null;
        // Wir setzen die Enemydistance auf den höchstmöglichen Wert, wir werden das Später überschreiben.
        float minimalEnemyDistance = float.MaxValue;
        // Wir führen etwas für jeden einzelnen Gegner der in Reichweite ist aus.
        foreach (GameObject enemy in enemiesInRange)
        {
            if(enemy != null)
            {
                // und zwar berechnen wir erstmal wie weit er vom Ziel entfernt ist.
                float distanceToGoal = enemy.GetComponent<Gegner>().DistanceToGoal();
                // Wenn der aktuelle gegner näher am Ziel ist als minimalEnemyDistance
                if (distanceToGoal < minimalEnemyDistance)
                {
                    // ist dieser Gegner das neue Ziel
                    target = enemy;
                    // Und wir setzen minimalEnemyDistance auf eben den neuen wert. 
                    // das machen wir um eben den Gegner zu finden, der am nahesten am Ziel (Schatulle) ist.
                    minimalEnemyDistance = distanceToGoal;
                }
            }
            
        }
        // Wenn das Ziel noch existiert und nicht z.B. durch andere Bullets in der Zwischenzeit zerstört wurde
        if (target != null)
        {
            
            
            // Überprüfen wir ob bereits Zeit für den nächsten Schuss ist
            // Das ist abhängig von der Firerate und wann zuletzt geschossen wurde
            if (Time.time - lastShotTime > tankData.currentLevel.fireRate)
            {
                // Wenn es Zeit ist, dann wird geschossen, und zwar auf unser Ziel
                Shoot(target.GetComponent<Collider>());
                // Und lastShotTime wird auf die aktuelle Zeit gesetzt.
                lastShotTime = Time.time;
            }
            //Beide Bogenturm Upgrades (davon die Bogenschützen) drehen sich mit
            grandChild1 = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
            grandChild2 = this.gameObject.transform.GetChild(1).GetChild(0).gameObject;
            if (grandChild1 != null && grandChild2 != null)
            {
                //Bogenschütze vom Lvl1-Turm ansprechen

                Vector3 direction = grandChild1.transform.position - target.transform.position;
                grandChild1.transform.rotation = Quaternion.AngleAxis(
                    Mathf.Atan2(direction.x, direction.z) * 180 / Mathf.PI, new Vector3(0, 1, 0));
                //Bogenschütze vom Lvl2-Turm ansprechen
                grandChild2.transform.rotation = grandChild1.transform.rotation;

            }
        }


    }

    // Wenn ein Objekt in unseren Collider reinkommt, wird das ausgeführt.
    private void OnTriggerEnter(Collider other)
    {
        // Wenn das Objekt das reinkommt den Tag "Enemy" hat, fügen wir es zu den Gegnern in Reichweite hinzu
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }
    // Wenn ein Objekt aus unserem Collider rausgeht, wird das ausgeführt.
    void OnTriggerExit(Collider other)
    {
        // Wenn das Objekt das rausgeht den Tag "Enemy" hat, löschen wir es aus den Gegnern in Reichweite 
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject == null)
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    // Die Schuss Methode in der eine Kugel mit der richtigen Richtung erzeugt wird
    public void Shoot(Collider target)
    {
        // Wir holen die Kugel aus dem Prefab, das für das Tank Level eingetragen wurde.
       GameObject bulletPrefab = tankData.currentLevel.bullet;
        // Ermitteln die Start und Zielposition
        Vector3 startPosition = grandChild1.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.y += 0.8f;
        targetPosition.y += 1f;
        targetPosition.x += 0.5f;
        // Erzeugen nun das Bullet innerhalb des SPiels
       GameObject newBullet = Instantiate(bulletPrefab);
        // Setzen die Kugel auf die Startposition
        newBullet.transform.position = startPosition;
        // Holen uns das Script "Bullet" der Kugel
       Bullet bulletComp = newBullet.GetComponent<Bullet>();
        // Setzen sein Ziel, seine Startposition und die Zielposition.
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        AudioManager.instance.shoot.Play();

    }
}
