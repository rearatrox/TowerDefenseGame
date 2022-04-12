using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gegner : MonoBehaviour
{

    public GameObject[] Wegpunkte;
    private int currentWaypoint = 0;
    GameManager gM;
    //Verstrichene Zeit seit letztem Wegpunkt
    private float lastWegpunktVergangeneZeit;
    public float speed;
    public float smoothness = 1f;

    // Start is called before the first frame update
    void Start()
    {

        Wegpunkte[0] = GameObject.FindGameObjectWithTag("Weg1");
        Wegpunkte[1] = GameObject.FindGameObjectWithTag("Weg2");
        Wegpunkte[2] = GameObject.FindGameObjectWithTag("Weg3");
        Wegpunkte[3] = GameObject.FindGameObjectWithTag("Weg4");
        Wegpunkte[4] = GameObject.FindGameObjectWithTag("Weg5");
        Wegpunkte[5] = GameObject.FindGameObjectWithTag("Weg6");
        Wegpunkte[6] = GameObject.FindGameObjectWithTag("Weg7");
        Wegpunkte[7] = GameObject.FindGameObjectWithTag("Weg8");
        Wegpunkte[8] = GameObject.FindGameObjectWithTag("Weg9");
        Wegpunkte[9] = GameObject.FindGameObjectWithTag("Weg10");
        Wegpunkte[10] = GameObject.FindGameObjectWithTag("Weg11");
        Wegpunkte[11] = GameObject.Find("Wegpunkt (11)");
        Wegpunkte[12] = GameObject.Find("Wegpunkt (12)");
        Wegpunkte[13] = GameObject.Find("Wegpunkt (13)");
        Wegpunkte[14] = GameObject.Find("Wegpunkt (14)");


        //Quaternion targetRotation = Drehrichtung[0].transform.rotation.eulerAngles.x;
        lastWegpunktVergangeneZeit = Time.time;
        gM = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 startPos = Wegpunkte[currentWaypoint].transform.position;
        Vector3 endPos = Wegpunkte[currentWaypoint + 1].transform.position;
        float Distanz = (startPos - endPos).magnitude;
        float totalTimeForPath = Distanz / speed;
        float currentTimeOnPath = Time.time - lastWegpunktVergangeneZeit;
        gameObject.transform.position = Vector3.Lerp(startPos, endPos,
            currentTimeOnPath / (totalTimeForPath * FindObjectOfType<GameManager>().SpeedFaktorGegner));

        if (gameObject.transform.position.Equals(endPos))
        {
            if(currentWaypoint < Wegpunkte.Length - 2)
              {
                  currentWaypoint++;
                  lastWegpunktVergangeneZeit = Time.time;
                Drehung();
            }
              else
              {
                  Destroy(gameObject);
                FindObjectOfType<GameManager>().toteGegner++;
                FindObjectOfType<GameManager>().SetLeben(FindObjectOfType<GameManager>().Leben - 1);
              }
        }

        LebensleisteDrehen();

    }

    void Drehung()
    {
        Vector3 StartPos = Wegpunkte[currentWaypoint].transform.position;
        Vector3 EndPos = Wegpunkte[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (StartPos - EndPos);

        float x = newDirection.x;
        float z = newDirection.z;

        float rotAngle = Mathf.Atan2(x, z) * 180 / Mathf.PI;

        transform.rotation = Quaternion.AngleAxis(rotAngle, new Vector3(0, 1, 0));

    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(gameObject.transform.position,
            Wegpunkte[currentWaypoint + 1].transform.position);

        for(int i = currentWaypoint + 1; i<Wegpunkte.Length - 1; i++)
        {
            Vector3 startPosition = Wegpunkte[i].transform.position;
            Vector3 endPosition = Wegpunkte[i+1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }

        return distance;
    }

    public void LebensleisteDrehen()
    {
        this.transform.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(FindObjectOfType<KameraRotator>().yAngle,
          FindObjectOfType<KameraRotator>().xAngle, 0);
    }
}
